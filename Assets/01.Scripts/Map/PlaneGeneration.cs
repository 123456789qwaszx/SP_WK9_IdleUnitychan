using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{
    public Dictionary<string, Vector3> objDuplicateCheck = new Dictionary<string, Vector3>();

    public GameObject plane;

    [SerializeField]
    private int radius = 5;
    private int planeOffset;

    private Vector3 startPos = Vector3.zero;

    private int xPlayerMove => (int)(CharacterManager.Instance.Player.transform.position.x - startPos.x);
    private int zPlayerMove => (int)(CharacterManager.Instance.Player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(CharacterManager.Instance.Player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(CharacterManager.Instance.Player.transform.position.z / planeOffset) * planeOffset;

    void Start()
    {
        planeOffset = radius * 2;

        if (startPos == Vector3.zero)
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * planeOffset + XPlayerLocation, 0, z * planeOffset + ZPlayerLocation);

                    if (!GameManager.Instance.tilePlane.ContainsValue(pos))
                    {
                        GameObject _plane = PoolManager.Instance.Pop(plane);
                        _plane.transform.position = pos;
                        _plane.transform.rotation = Quaternion.identity;

                        GameManager.Instance.tilePlane.Add($"{pos}", _plane.transform.position);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (hasPlayerMoved())
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * planeOffset + XPlayerLocation, 0, z * planeOffset + ZPlayerLocation);

                    if (!GameManager.Instance.tilePlane.ContainsValue(pos))
                    {
                        GameObject _plane = PoolManager.Instance.Pop(plane);
                        _plane.transform.position = pos;
                        _plane.transform.rotation = Quaternion.identity;

                        GameManager.Instance.newPlaneForOBJ.Add(pos);

                        GameManager.Instance.tilePlane.Add($"{pos}", _plane.transform.position);
                        SpawnObj();
                    }
                }
            }
        }
    }

    bool hasPlayerMoved()
    {
        if (Mathf.Abs(xPlayerMove) >= planeOffset || Mathf.Abs(zPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }

    // 이 부분을 GameObject 대신 Resource로 넣고, 그것의 타입에 따라 게임매니저에서 호출할 것을 정해주고 싶다.
    // switch (go.data.resourcetype)
    //     {
    //         case ResourceType.Tree:
    //             prefab = GameManager.Instance.SpawnTree();
    //             go.hitCount = 3; // 스크립터블 오브젝트의 데이터를 가져오고
    //             treeCount++; // (delegate로 게임매니저에 알려주기)
    //             break;
    //         case ResourceType.Rock:
    //             prefab = GameManager.Instance.SpawnRock();
    //             go.hitCount = 5;
    //             rockCount++; // (delegate로 게임매니저에 알려주기)
    //             break;
    private void SpawnObj()
    {
        int randomPlaneSelection = Random.Range(0, GameManager.Instance.newPlaneForOBJ.Count);
        
        Vector3 randomPlanePos = GameManager.Instance.newPlaneForOBJ[randomPlaneSelection];

        int randomResourceSelection = Random.Range(0, 100);

        if (!objDuplicateCheck.ContainsValue(randomPlanePos))
        {
            if (randomResourceSelection <= 50)
            {
                GameObject go = GameManager.Instance.SpawnTree();
                go.transform.position = randomPlanePos;

                objDuplicateCheck.Add($"{go.transform.position}", randomPlanePos);
            }
            else
            {
                return;
            }
        }
        
        GameManager.Instance.newPlaneForOBJ.Clear();
    }
}
