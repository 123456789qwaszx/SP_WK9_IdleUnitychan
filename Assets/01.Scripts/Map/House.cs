using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public PlayerInteraction interaction;
    private float workSpeed = 0.5f;

    [SerializeField]
    float amount = 50;

    void Start()
    {
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerInteraction;
    }

    void OnPlayerInteraction(Player pc)
    {
        //CharacterManager.Instance.Player.condition.Health.Add(amount);
    }
}