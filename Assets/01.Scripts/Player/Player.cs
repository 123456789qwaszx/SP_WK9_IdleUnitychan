using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController controller;
    PlayerCondition condition;
    PlayerBehaviour behaviour;

    void Awake()
    {
        Managers.Char.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        behaviour = GetComponent<PlayerBehaviour>();
    }
}
