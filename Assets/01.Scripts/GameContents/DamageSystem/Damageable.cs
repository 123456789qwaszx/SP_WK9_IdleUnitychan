using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public enum MessageType
{
    DAMAGED,
    DEAD,
    RESPAWN,
}

public interface IMessageReceiver
{
    void OnReceiveMessage(MessageType type, object sender, object msg);
}


public class EnforceTypeAttribute : PropertyAttribute
{
    public System.Type type;

    public EnforceTypeAttribute(System.Type enforcedType)
    {
        type = enforcedType;
    }
}


public class Damageable : MonoBehaviour
{

    public struct DamageMessage
    {
        public MonoBehaviour damager;
        public int amount;
        public Vector3 damageSource;
    }

    public int maxHitPoints;
    public float invulnerabiltyTime;

    public bool isInvulnerable { get; set; }
    public int currentHitPoints { get; private set; }

    public UnityEvent OnDeath, OnReceiveDamage, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;

    protected float _timeSinceLastHit = 0.0f;
    protected Collider _collider;

    [EnforceType(typeof(IMessageReceiver))]
    public List<MonoBehaviour> onDamageMessageReceivers;

    Action schedule;
    // 구조를 잘못짜니 이렇게 하나하나 붙여야 되네.
    public EnemyData _enemyData;

    void OnEnable()
    {
        ResetDamage();
    }

    void Start()
    {
        ResetDamage();
        _collider = GetComponent<Collider>();
    }

    public void ResetDamage()
    {
        currentHitPoints = maxHitPoints;
        isInvulnerable = false;
        _timeSinceLastHit = 0.0f;

        OnResetDamage.Invoke();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            _timeSinceLastHit += Time.deltaTime;
            if (_timeSinceLastHit > invulnerabiltyTime)
            {
                _timeSinceLastHit = 0.0f;
                isInvulnerable = false;
                OnBecomeVulnerable.Invoke();
            }
        }
    }


    public void SetColliderState(bool enabled)
    {
        _collider.enabled = enabled;
    }


    public void ApplyDamage(DamageMessage data)
    {
        if (currentHitPoints <= 0)
        {// 이미 죽은 상태, 대미지 무시
            return;
        }

        if (isInvulnerable)
        {// 대미지 판정은 없지만, 타격판정은 있음
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        Vector3 forward = transform.forward;

        Vector3 positionToDamager = data.damageSource - transform.position;
        positionToDamager -= transform.up * Vector3.Dot(transform.up, positionToDamager);

        isInvulnerable = true;
        currentHitPoints -= data.amount;

        if (currentHitPoints <= 0)
            schedule += OnDeath.Invoke; // 오브젝트들이 죽고나서 hit되는 것 예방
        else
            OnReceiveDamage.Invoke();

        Debug.Log("ApplyDamage!!");
        if (currentHitPoints == 0)
        {
            PoolManager.Instance.Push(gameObject);

        switch (_enemyData.enemytype)
            {
                case EnemyType.Bear:
                    SpawningPool.Instance._bearCount -= 1;
                    break;
                case EnemyType.Skeleton:
                    SpawningPool.Instance._skeletonCount -= 1;
                    break;
                case EnemyType.Orc:
                    SpawningPool.Instance._orcCount -= 1;
                    break;
                default:
                    break;
            }
        }

        //var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

            // 데미지 판정을 다른 오브젝트에게 전달
            // for (var i = 0; i < onDamageMessageReceivers.Count; ++i)
            // {
            //     var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
            //     receiver.OnReceiveMessage(messageType, this, data);
            //     Debug.Log(onDamageMessageReceivers.Count);
            // }
    }

    void LateUpdate()
    {
        if (schedule != null)
        {
            schedule();
            schedule = null;
        }
    }
}