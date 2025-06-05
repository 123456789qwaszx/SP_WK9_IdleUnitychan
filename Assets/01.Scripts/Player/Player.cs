using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerBehaviour behaviour;

    void Awake()
    {
        Managers.Char.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        behaviour = GetComponent<PlayerBehaviour>();
    }
}
