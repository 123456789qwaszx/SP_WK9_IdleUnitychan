using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer
{
    Ground = 6,
    Monster = 7,
    Wall = 8,
    Item = 9
}

public class GameManager : Singleton<GameManager>
{
    // 현재 생성 된 모든 타일맵의 좌표를 가지는 리스트
    public Dictionary<string, Vector3> tilePlane = new Dictionary<string, Vector3>();

    // 타일 생성 시, 새롭게 생성된 타일의 좌표를 가지는 리스트. Obcj 생성 후 초기화
    public List<Vector3> newPlaneForOBJ = new List<Vector3>();

    // EnemyScanner에서 찾은 몬스터를 받아오는 리스트
    public List<Transform> detected = new List<Transform>();

    //MouseDragStick에서 마우스의 움직인 방향을 받아옴
    public Vector2 MouseDir { get; set; } = Vector2.zero;

    public bool JumpInput;
    public bool IdleMode;

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
