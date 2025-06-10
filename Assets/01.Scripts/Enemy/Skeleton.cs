using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IMessageReceiver
{
    [SerializeField]
    EnemyData data;

    protected Player _mTarget = null;
    protected MonsterController _mController;

    public Player _MTarget { get { return _mTarget; } }
    public MonsterController _MController { get { return _mController; } }

    public TargetScanner playerScanner;

    public AIState currentState;



    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case MessageType.DEAD:
                Death((Damageable.DamageMessage)msg);
                break;
            case MessageType.DAMAGED:
                ApplyDamage((Damageable.DamageMessage)msg);
                break;
            default:
                break;
        }
    }

    public void Death(Damageable.DamageMessage msg)
    {
        PoolManager.Instance.Push(gameObject);

        // 사운드 재생
        // 죽는 애니메이션
        // 튕겨올라갔다 땅에 떨어지는 좌표이동
    }

    public void ApplyDamage(Damageable.DamageMessage msg)
    {

    }

    public void Attack()
    {
        //Controller.animator.SetBool("m_HasAttack")
    }

    public void FindTarget()
    {
        _mTarget = playerScanner.Detect(transform, _mTarget = null);
    }
}
