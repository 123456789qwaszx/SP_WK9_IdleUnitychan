using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameObject blockGameObject;

    private int worldSizeX = 10;

    private int worldSizeZ = 10;

    private int noiseHeight = 3;

    private float gridOffset = 1.1f;

    void Start()
    {
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * gridOffset, generateNoise(x,z,8f) * noiseHeight, z * gridOffset);

                GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;

                block.transform.SetParent(this.transform);
            }
        }
    }

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
