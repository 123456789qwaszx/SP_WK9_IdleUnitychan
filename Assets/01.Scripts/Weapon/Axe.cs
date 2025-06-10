using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int _damage = 1;
    public Player _owner;

    //test용
    void Start()
    {
        SetOwner(CharacterManager.Instance.Player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Axe데미지!");
            CheckDamage(other);
        }
    }

    public void SetOwner(Player owner)
    {
        _owner = owner;
    }


    private void CheckDamage(Collider other)
    {
        Damageable d = other.GetComponent<Damageable>();
        Damageable.DamageMessage data;

        data.amount = _damage;
        data.damager = this;
        data.damageSource = _owner.transform.position;
        
        Debug.Log("CheckDamage!");

        d.ApplyDamage(data);
    }
}
