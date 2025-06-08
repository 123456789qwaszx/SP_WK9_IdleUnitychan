using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject block;
    public Player player;
    
    [SerializeField]
    private Transform _root;

    void Start()
    {
        player = CharacterManager.Instance.Player;
        SpawnTile();
    }

    void Update()
    {

    }

    void SpawnTile()
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject go = Instantiate(block);
            go.transform.SetParent(_root);

            int x = i % 6 ;
            int z = i / 6 ;

            go.transform.position = new Vector3(x* 6-18, 0, z* 6-18);
            go.name = $"{x + "_" + z}";
        }

        // for (int i = 0; i < 20; i++)
        // {
        //     yield return new WaitForSeconds(1f);

        //     GameObject go = RemoveFromPile();
        //     if ( go != null)
        //     PoolManager.Instance.Push(tile);
        // }
    }
}
