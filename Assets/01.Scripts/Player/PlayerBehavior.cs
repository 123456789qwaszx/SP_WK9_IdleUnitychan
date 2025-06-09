using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class PlayerBehavior : MonoBehaviour, IDamageable
{
    IDamageable idamagable;

    void Awake()
    {
        idamagable = GetComponent<IDamageable>();
    }

    void Start()
    {

    }

    void Update()
    {
        Stop();
    }

    
    public void TakeDamage(float damage)
    {
        Debug.Log("공격당함");
    }

    void UpdateMoving()
    {
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

        // nma.CalculatePath
        // nma.Move() // 실제 크기까지 포함하는 방향벡터

    }


    void Stop()
    {
        Vector3 qwe = new Vector3(GameManager.Instance.MouseDir.x, 0, GameManager.Instance.MouseDir.y);
        Debug.DrawRay(CharacterManager.Instance.Player.transform.position + Vector3.up * 0.5f, qwe.normalized * 15f, Color.green);
        if (Physics.Raycast(CharacterManager.Instance.Player.transform.position + Vector3.up * 0.5f, GameManager.Instance.MouseDir, 1.0f, LayerMask.GetMask("Wall")))
        {
            Debug.Log("StopAndJump");
        }
        // Dir.y이다보니 위로 쏘는데, 그걸 z로 바꿔주는 작업이 필요.

    }
}

