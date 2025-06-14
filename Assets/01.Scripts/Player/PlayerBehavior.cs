using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerBehavior : MonoBehaviour
{
    Animator anim;

    public PlayerState _state;

    int _mask = (1 << (int)Layer.Ground) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Item);

    GameObject _lockTarget;
    Vector3 _destPos;

    float _attackDitance = 3;

    public GameObject enemy { get { return _target; } }

    protected GameObject _target = null;
    public EnemyScanner enemyScanner;

    protected Damageable _Damageable;


    void OnEnable()
    {
        _Damageable = GetComponent<Damageable>();
        _Damageable.onDamageMessageReceivers.Add(this);

        _Damageable.isInvulnerable = true;
    }


    void OnDisable()
    {
        _Damageable.onDamageMessageReceivers.Remove(this);
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        enemyScanner = GetComponent<EnemyScanner>();

        _attackDitance = 3;
    }

    
    public void StateClear()
    {
        anim.SetBool("Attack", false);
    }

    public void FindEnemy()
    {
        anim.SetBool("Attack", false);
        GameManager.Instance.detected.Clear();
        enemyScanner.FindVisibleTargets();

        foreach (var go in GameManager.Instance.detected)
        {
            Vector3 targetPosition = go.Value;
            if (GameManager.Instance.detected.Count >= 0)
            {
            Debug.Log(targetPosition);
            // UI 적발견!!
            Vector3 toTarget = targetPosition - transform.position;
            toTarget.y = 0;

            if (toTarget.sqrMagnitude < _attackDitance * _attackDitance)
            {
                //anim.SetTrigger("Attack");
                anim.SetBool("Attack", true);
            }
            else
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                float moveDist = Mathf.Clamp(CharacterManager.Instance.Player.stat.MoveSpeed * Time.deltaTime, 0, toTarget.magnitude);
                nma.Move(toTarget.normalized * moveDist);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toTarget), 20 * Time.deltaTime);
            }
        
        }

        Debug.Log("적을 찾지 못했습니다. 다음 장소로 이동합니다");
        //다음 이동경로 탐색
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        anim.SetBool("Attack", false);

        _state = PlayerState.Idle;
    }


    public void OnLockOn(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        if (context.phase == InputActionPhase.Started)
        {
            if (raycastHit)
            {
                _destPos = hit.point;
                _state = PlayerState.Moving;

                if (Physics.Raycast(ray, out hit, 100.0f, _mask))
                    _lockTarget = hit.collider.gameObject;
                else
                    _lockTarget = null;
            }
        }
        if (context.phase == InputActionPhase.Performed)
        {
            if (_lockTarget != null)
                _destPos = _lockTarget.transform.position;
            else if (raycastHit)
                _destPos = hit.point;
        }
        // if (context.phase == InputActionPhase.Canceled)
        // {
        //     _lockTarget = null;
        // }

    }




    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GameManager.Instance.JumpInput = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            GameManager.Instance.JumpInput = false;
        }
    }


    public void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackDitance)
            {
                _state = PlayerState.Skill;
                anim.SetFloat("Speed", 1f);
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(CharacterManager.Instance.Player.stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }


    void Stop()
    {
        Vector3 qwe = new Vector3(GameManager.Instance.MouseDir.x, 0, GameManager.Instance.MouseDir.y);
        Debug.DrawRay(CharacterManager.Instance.Player.transform.position + Vector3.up * 0.5f, qwe.normalized * 15f, Color.green);
        if (Physics.Raycast(CharacterManager.Instance.Player.transform.position + Vector3.up * 0.5f, GameManager.Instance.MouseDir, 1.0f, LayerMask.GetMask("Wall")))
        {
            Debug.Log("StopAndJump");
            _state = PlayerState.Idle;

            GameManager.Instance.JumpInput = true;
        }
    }


}