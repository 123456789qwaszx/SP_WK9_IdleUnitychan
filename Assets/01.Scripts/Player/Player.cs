using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Die,
    Idle,
    Moving,
    Pursuit,
    Skill,
}

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerBehavior behavior;
    public PlayerStat stat;


    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        behavior = GetComponent<PlayerBehavior>();
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
            behavior.FindEnemy();
            
            switch (behavior._state)
            {
                case PlayerState.Die:
                    behavior.UpdateDie();
                    break;
                case PlayerState.Idle:
                    behavior.UpdateIdle();
                    break;
                case PlayerState.Moving:
                    behavior.UpdateMoving();
                    break;
                case PlayerState.Skill:
                    behavior.UpdateSkill();
                    break;
            }
        }

        controller.AnimationJump();
    }
}
