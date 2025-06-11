using System;
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
    public PlayerInteractions interaction;


    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        behavior = GetComponent<PlayerBehavior>();
        stat = GetComponent<PlayerStat>();
        interaction = GetComponent<PlayerInteractions>();
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IdleMode == false)
        {
            controller.Move();
            controller.Jump();
        }
        controller.AnimationCalculate();

        if (GameManager.Instance.IdleMode)
        {
            behavior.FindEnemy();
        }
    }
}
