using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpawningPool : Singleton<SpawningPool>
{
    [SerializeField]
    private Enemy _bear;
    [SerializeField]
    private Enemy _orc;
    [SerializeField]
    private Enemy _skeleton;

    [Header("Bear")]
    [SerializeField]
    public int _bearCount = 0;
    [SerializeField]
    int _keepBearCount = 0;
    [SerializeField]
    float _bearSpawnTime = 10f;
    
    [Header("Orc")]
    [SerializeField]
    public int _orcCount = 0;
    [SerializeField]
    int _keepOrcCount = 0;
    [SerializeField]
    float _orcSpawnTime = 10f;

    [Header("Skeleton")]
    [SerializeField]
    public int _skeletonCount = 0;
    [SerializeField]
    int _keepSkeletonCount = 0;
    [SerializeField]
    float _skeletonSpawnTime = 10f;


    int _reserveCount = 0;
    Vector3 _spawnPos;


    void Update()
    {
        while (_reserveCount + _bearCount < _keepBearCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_bear, _bearSpawnTime));
        }
        while (_reserveCount + _orcCount < _keepOrcCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_orc, _orcSpawnTime));
        }
        while (_reserveCount + _skeletonCount < _keepSkeletonCount)
            {
                StartCoroutine(CoOBJresourceSpawn(_skeleton, _skeletonSpawnTime));
            }
    }
    
    IEnumerator CoOBJresourceSpawn(Enemy prefab, float spawnTime)
    {
        _reserveCount++;

        yield return new WaitForSeconds(spawnTime);

        GameObject go;
        
        Vector3 randPos;
        Vector3 randDir = /*CharacterManager.Instance.Player.transform.position +*/ Random.insideUnitSphere * Random.Range(0, 50);
        randDir.y = 0;
        randPos = _spawnPos + randDir;
    
        switch (prefab.enemyData.enemytype)
        {
            case EnemyType.Bear:
                go = GameManager.Instance.SpawnBear();
                go.transform.position = randPos;
                _bearCount++;
                break;
            case EnemyType.Orc:
                go = GameManager.Instance.SpawnOrc();
                go.transform.position = randPos;
                _orcCount++;
                break;
            case EnemyType.Skeleton:
                go = GameManager.Instance.SpawnSkeleton();
                go.transform.position = randPos;
                _skeletonCount++;
                break;
            default:
                break;
        }

        _reserveCount--;
    }
}
