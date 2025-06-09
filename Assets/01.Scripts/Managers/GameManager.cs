using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<string, Vector3> tilePlane = new Dictionary<string, Vector3>();

    public List<Vector3> newPlaneForOBJ = new List<Vector3>();

    public Vector2 MouseDir { get; set; } = Vector2.zero;

    public bool JumpInput;

    public GameObject bear_Prefab;
    public GameObject SpawnBear() { return PoolManager.Instance.Pop(bear_Prefab); }
    public void DespawnBear(GameObject bear) { PoolManager.Instance.Push(bear); }

    public GameObject tree_Prefab;
    public GameObject SpawnTree() { return PoolManager.Instance.Pop(tree_Prefab); }
    public void DeSpawnTree(GameObject tree) { PoolManager.Instance.Push(tree); }
}
