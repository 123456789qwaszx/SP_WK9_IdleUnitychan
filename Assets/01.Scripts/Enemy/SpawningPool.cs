using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    private NPC _bear;

    [Header("Bear")]
    [SerializeField]
    public int _bearCount = 0;
    [SerializeField]
    int _keepBearCount = 2;
    [SerializeField]
    float _bearSpawnTime = 3f;

    int _reserveCount = 0;
    Vector3 _spawnPos;


    void Update()
    {
        while (_reserveCount + _bearCount < _keepBearCount)
        {
            StartCoroutine(CoOBJresourceSpawn( _bearSpawnTime));
        }
    }
    
    IEnumerator CoOBJresourceSpawn(float spawnTime)
    {
        _reserveCount++;

        yield return new WaitForSeconds(spawnTime);
        GameObject go = GameManager.Instance.SpawnBear();
        _bearCount++;

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, 50);
        randDir.y = 0;
        randPos = _spawnPos + randDir;

        go.transform.position = randPos;
        _reserveCount--;
    }
}
