using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    public Transform _resourceRoot;

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
            StartCoroutine(CoOBJresourceSpawn(_bear, _bearSpawnTime));
        }
    }
    
    IEnumerator CoOBJresourceSpawn(NPC prefab, float spawnTime)
    {
        _reserveCount++;

        yield return new WaitForSeconds(spawnTime);
        NPC go = Instantiate(prefab);
        _bearCount++;

        go.name = prefab.name;
        go.transform.parent = _resourceRoot;

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, 50);
        randDir.y = 0;
        randPos = _spawnPos + randDir;

        go.transform.position = randPos;
        _reserveCount--;
    }
}
