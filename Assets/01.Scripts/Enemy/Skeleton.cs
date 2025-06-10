using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField]
    EnemyData data;

    protected Player _target = null;
    protected MonsterController _controller;

    public Player Target { get { return _target; } }
    public MonsterController Controller { get { return _controller; } }

    public TargetScanner playerScanner;


    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DEAD:
                Death((DamageMessage)msg);
                break;
            case MessageType.DAMAGED:
                ApplyDamage((DamageMessage)msg);
                break;
            default:
                break;
        }
    }

    public void Death(DamageMessage msg)
    {

    }

    public void ApplyDamage(DamageMessage msg)
    {
        
    }
}
