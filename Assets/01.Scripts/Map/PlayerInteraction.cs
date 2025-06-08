using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerInteraction : MonoBehaviour
{
    public Action<Player> OnPlayerInteraction;
    public float InteractInterval = 0.5f;
    private Player _player;

    private void Start()
    {
        StartCoroutine(CoPlayerInteraction());
    }

    IEnumerator CoPlayerInteraction()
    {
        while (true)
        {
            yield return new WaitForSeconds(InteractInterval);

            if (_player != null)
                OnPlayerInteraction(_player);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Player pc = other.GetComponent<Player>();
        if (pc == null)
            return;

        _player = pc;
    }

    void OnTriggerExit(Collider other)
    {
        Player pc = other.GetComponent<Player>();
        if (pc == null)
            return;

        _player = null;
    }
}