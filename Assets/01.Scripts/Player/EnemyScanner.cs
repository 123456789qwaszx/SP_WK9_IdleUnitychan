using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public float detectionRadius = 10;
    [Range(0.0f, 360.0f)]
    public float detectionAngle = 360;
    public LayerMask viewBlockerLayerMask;
    public Dectable _enemy;

    public Dectable DetectEnemy(Player player)
    {
        Vector3 toEnemy = _enemy.transform.position - CharacterManager.Instance.Player.transform.position;

        toEnemy.y = 0;

        // 이거 너무 멀어진 블록이나 오브젝트 제거할 때도 쓸 수 있겠다.
        // 완전 대박이야. 어떻게 이런 기능이 있을 수 있지?
        if (toEnemy.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            // 대박. 역시 수학을 잘하니 편하구나. 이렇게 내적을 구하는 거구나
            if (Vector3.Dot(toEnemy.normalized, player.transform.forward) >
                // 내적 값이, 시야각/2의 Cos값보다 큰지 체크 
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                bool canSee = false;

                Debug.DrawRay(CharacterManager.Instance.Player.transform.position, toEnemy, Color.blue);

                canSee |= !Physics.Raycast(CharacterManager.Instance.Player.transform.position, toEnemy.normalized, detectionRadius,
                        viewBlockerLayerMask, QueryTriggerInteraction.Ignore);
                        
                if (canSee)
                    return _enemy;
            }
        }
        return null;
    }
}
