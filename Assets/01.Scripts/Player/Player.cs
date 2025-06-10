using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Die,
    Idle,
    Moving,
    Skill,
}

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerBehavior behaviour;
    public PlayerStat stat;

    public PlayerState _state;


    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        behaviour = GetComponent<PlayerBehavior>();
        stat = GetComponent<PlayerStat>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (GameManager.Instance.IdleMode == false)
            {
                GameManager.Instance.IdleMode = true;
            }
            else
            {
                GameManager.Instance.IdleMode = false;
            }
        }
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IdleMode == false)
        {
            controller.Move();
            controller.Jump();
        }
        else
        {
            switch (_state)
            {
                case PlayerState.Die:
                behaviour.UpdateDie();
                break;
            case PlayerState.Idle:
                behaviour.UpdateIdle();
                break;
            case PlayerState.Moving:
                behaviour.UpdateMoving();
                break;
            case PlayerState.Skill:
                behaviour.UpdateSkill();
                break;
            }
        }
    }
}
