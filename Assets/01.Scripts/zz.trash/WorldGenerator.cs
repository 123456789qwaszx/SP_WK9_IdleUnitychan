using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static List<GameObject> GeneratedTiles = new List<GameObject>();

    [SerializeField]
    private GameObject tilePrefab;

    private int radius = 10;

    void Start()
    {
        Paths pathGenerator = new Paths(radius);
        for (int x = 0; x < radius; x++)
        {
            for (int z = -0; z < radius; z++)
            {
                GameObject tile = PoolManager.Instance.Pop(tilePrefab);
                tile.transform.position = new Vector3(x * 1.5f, 0, z * 1.5f);
                tile.transform.rotation = Quaternion.identity;

                GeneratedTiles.Add(tile);
                pathGenerator.AssignTopAndBottomTiles(z, tile);
            }
        }

        pathGenerator.GeneratePath();
        foreach (var pObject in pathGenerator.GetPath())
        {
            pObject.SetActive(false);
        }
    }

}
