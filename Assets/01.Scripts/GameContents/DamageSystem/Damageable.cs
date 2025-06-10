using UnityEngine;
using UnityEngine.Events;
using System;

public enum MessageType
{
    DAMAGED,
    DEAD,
    RESPAWN,
    //Add your user defined message type after
}

public interface IMessageReceiver
{
    void OnReceiveMessage(MessageType type, object sender, object msg);
}

public struct DamageMessage
{
    public MonoBehaviour damager;
    public int amount;
    public Vector3 direction;
    public Vector3 damageSource;
    public bool throwing;

    public bool stopCamera;
}

public class Damageable : MonoBehaviour
{
    public int maxHitPoints;
    public float invulnerabiltyTime;

    public bool isInvulnerable { get; set; }
    public int currentHitPoints { get; private set; }

    public UnityEvent OnDeath, OnReceiveDamage, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;

    protected float _timeSinceLastHit = 0.0f;
    protected Collider _collider;

    Action schedule;

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

        var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;
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