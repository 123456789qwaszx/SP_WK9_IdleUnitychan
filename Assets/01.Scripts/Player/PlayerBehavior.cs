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
    attack,
    Skill,
}


public class PlayerBehavior : MonoBehaviour, IDamageable
{
    IDamageable idamagable;

    public PlayerState _state;

    public float checkRate = 0.1f;
    public float lastCheckTime;

    int _mask = (1 << (int)Layer.Ground) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Item);

    Texture2D _attackIcon;
    Texture2D _HandIcon;
    Texture2D _LootIcon;
    public bool IsHandCursor;


    GameObject _lockTarget;
    Vector3 _destPos;

    bool _canAttack;
    float _attackLimit = 1;


    void Awake()
    {
        idamagable = GetComponent<IDamageable>();
    }


    void Start()
    {
        checkRate = 0.1f;

        _attackIcon = Resources.Load<Texture2D>("Cursor/Cursor_Attack");
        _LootIcon = Resources.Load<Texture2D>("Cursor/Cursor_Loot");
        _HandIcon = Resources.Load<Texture2D>("Cursor/Cursor_Hand");

    }


    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            UpdateMouseCursor();
        }

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
            case PlayerState.attack:
                UpdateAttack();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
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
            else if(raycastHit)
                _destPos = hit.point;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            _lockTarget = null;
        }
        
    }


    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

        }
    }


    void UpdateSkill()
    {

    }


    void UpdateAttack()
    {

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


    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Layer.Monster)
            {
                Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                IsHandCursor = false;
            }
            else if (hit.collider.gameObject.layer == (int)Layer.Item)
            {
                Cursor.SetCursor(_LootIcon, new Vector2(_LootIcon.width / 3, 0), CursorMode.Auto);
                IsHandCursor = false;
            }
            else
            {
                Cursor.SetCursor(_HandIcon, Vector2.zero, CursorMode.Auto);
            }
        }

        if (!Physics.Raycast(ray, out hit, 100.0f, _mask) && (IsHandCursor == false))
        {
            Cursor.SetCursor(_HandIcon, Vector2.zero, CursorMode.Auto);
            IsHandCursor = true;
        }
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("공격당함");
    }
}

