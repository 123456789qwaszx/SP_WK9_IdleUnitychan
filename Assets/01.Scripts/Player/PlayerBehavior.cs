using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum Layer
{
    Ground = 6,
    Monster = 7,
    Wall = 8,
    Item = 9
}


public enum PlayerState
{
    Die,
    Idle,
    Moving,
    Skill,
}


public class PlayerBehavior : MonoBehaviour, IDamageable
{
    IDamageable idamagable;
    Animator anim;

    public PlayerState _state;

    int _mask = (1 << (int)Layer.Ground) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Item);


    GameObject _lockTarget;
    Vector3 _destPos;

    bool _canAttack;
    float _attackLimit = 1;


    void Start()
    {
        idamagable = GetComponent<IDamageable>();
        anim = GetComponent<Animator>();

        _attackLimit = 5;
    }


    void Update()
    {

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
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


    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

        }
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


    void UpdateSkill()
    {
        anim.SetBool("Attack", true);
    }


    void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackLimit)
            {
                _canAttack = true;
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


    void UpdateIdle()
    {
    }


    void UpdateDie()
    {

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


    public void TakeDamage(float damage)
    {
        Debug.Log("공격당함");
    }
}

