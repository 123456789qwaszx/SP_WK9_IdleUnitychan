using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Vector2 MouseDir { get; set; } = Vector2.zero;

    public bool JumpInput;

    public GameObject bear;
    public GameObject SpawnBear() { return PoolManager.Instance.Pop(bear); }
    public void DespawnBear(GameObject bear) { PoolManager.Instance.Push(bear); }
}
