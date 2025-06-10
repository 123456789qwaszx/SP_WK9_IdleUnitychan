using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    // public float heightOffset = 0.0f;
    // public float detectionRadius = 10;
    // [Range(0.0f, 360.0f)]
    // public float detectionAngle = 360;
    // public float maxHeightDifference = 1.0f;
    // public LayerMask viewBlockerLayerMask;
    // public Dectable _enemy;

    public float ViewAngle;    //시야각
    public float ViewDistance; //시야거리

    void Update()
    {
        DrawView();
        FinaVisibleTargets();
    }


    public void FinaVisibleTargets()
    {
        // 시야범위 내에서 MonsterLayer를 가진 콜라이더 가져오기
        // 이러면 저 시야 범위를 5~10으로 정할 수도 있나? 그러면 그걸로 바닥을 판단해서 지워버리면 진짜 편할 것 같은데 나중에 해봐야겠다.
        int _mask = 1 << (int)Layer.Monster;
        Collider[] targets = Physics.OverlapSphere(gameObject.transform.position, ViewDistance, _mask);

        for (int i = 0; i < targets.Length; i++)
        {
            Transform target = targets[i].transform;

            // 타겟 까지의 단위벡터
            Vector3 dirToTarget = (target.position - gameObject.transform.position).normalized;

            // 시야에 들어왔는지 체크
            if (Vector3.Dot(gameObject.transform.position, dirToTarget) > Mathf.Cos((ViewAngle / 2) * Mathf.Deg2Rad))
            {
                float distToTarget = Vector3.Distance(gameObject.transform.position, target.position);

                if (Physics.Raycast(gameObject.transform.position, dirToTarget, distToTarget, _mask))
                {
                    GameManager.Instance.detected.Add(target);
                }
            }
        }
    }


    public void DrawView()
    {
        Vector3 leftBoundary = DirFromAngle(-ViewAngle / 2);
        Vector3 rightBoundary = DirFromAngle(ViewAngle / 2);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + leftBoundary * ViewDistance, Color.green);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + rightBoundary * ViewDistance, Color.green);
    }


    public Vector3 DirFromAngle(float angleInDegrees)
    {
        // 좌우 회전값 갱신
        angleInDegrees += transform.eulerAngles.y;
        // 경계 벡터값 반환
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // public void DrawBoundary(GameObject go, Dectable dectable)
    // {
    //     Vector3 detectionRange = go.transform.position - dectable.transform.position;

    //     if (detectionRange.sqrMagnitude <= detectionRadius * detectionRadius) ;
    // }

    // public Dectable DetectEnemy()
    // {
    //     Vector3 toEnemy = _enemy.transform.position - CharacterManager.Instance.Player.transform.position;

    //     toEnemy.y = 0;

    //     // 이거 너무 멀어진 블록이나 오브젝트 제거할 때도 쓸 수 있겠다.
    //     // 완전 대박이야. 어떻게 이런 기능이 있을 수 있지?
    //     if (toEnemy.sqrMagnitude <= detectionRadius * detectionRadius)
    //     {
    //         // 대박. 역시 수학을 잘하니 편하구나. 이렇게 내적을 구하는 거구나
    //         if (Vector3.Dot(toEnemy.normalized, CharacterManager.Instance.Player.transform.forward) >
    //             // 내적 값이, 시야각/2의 Cos값보다 큰지 체크 
    //             Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
    //         {
    //             bool canSee = false;

    //             //Debug.DrawRay(CharacterManager.Instance.Player.transform.position, toEnemy, Color.blue);

    //             canSee |= !Physics.Raycast(CharacterManager.Instance.Player.transform.position, toEnemy.normalized, detectionRadius,
    //                     viewBlockerLayerMask, QueryTriggerInteraction.Ignore);

    //             if (canSee)
    //                 return _enemy;
    //         }
    //     }
    //     return null;
    // }
}
