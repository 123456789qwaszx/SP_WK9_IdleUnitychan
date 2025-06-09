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
    public void DeSpawnTree(GameObject tree_Prefab) { PoolManager.Instance.Push(tree_Prefab); }

    public GameObject rock_Prefab;
    public GameObject SpawnRock() { return PoolManager.Instance.Pop(rock_Prefab); }
    public void DeSpawnRock(GameObject rock_Prefab) { PoolManager.Instance.Push(rock_Prefab); }

    public GameObject Iron_Prefab;
    public GameObject SpawnIron() { return PoolManager.Instance.Pop(Iron_Prefab); }
    public void DeSpawnIron(GameObject Iron_Prefab) { PoolManager.Instance.Push(Iron_Prefab); }

    public GameObject mushroom_Prefab;
    public GameObject SpawnMushroom() { return PoolManager.Instance.Pop(mushroom_Prefab); }
    public void DeSpawnMushroom(GameObject mushroom_Prefab) { PoolManager.Instance.Push(mushroom_Prefab); }
}
