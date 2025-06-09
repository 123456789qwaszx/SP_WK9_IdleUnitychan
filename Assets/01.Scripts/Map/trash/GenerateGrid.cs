// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GenerateGrid : MonoBehaviour
// {
//     public GameObject blockGameObject;

//     public GameObject objectToSpawn;

//     public GameObject player;

//     private int worldSizeX = 10;

//     private int worldSizeZ = 10;

//     private int noiseHeight = 3;

//     private float gridOffset = 1.1f;

//     private Vector3 startPosition;

//     private Hashtable blockContainer = new Hashtable();

//     private List<Vector3> blockPositions = new List<Vector3>();

//     void Start()
//     {
//         for (int x = -worldSizeX; x < worldSizeX; x++)
//         {
//             for (int z = -worldSizeZ; z < worldSizeZ; z++)
//             {
//                 Vector3 pos = new Vector3(x * 1 + xPlayerLocation, GenerateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight, z * 1 + zPlayerLocation);

//                 if (!blockContainer.ContainsKey(pos))
//                 {
//                     //GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;
//                     GameObject block = PoolManager.Instance.Pop(blockGameObject);
//                     block.transform.position = pos;
//                     block.transform.rotation = Quaternion.identity;

//                     blockContainer.Add(pos, block);

//                     blockPositions.Add(block.transform.position);

//                     //block.transform.SetParent(this.transform);

//                 }
//             }
//         }
//         SpawnObject();
//     }

//     void Update()
//     {
//         //블록 사이즈보다 많이 움직였을 경우.
//         if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
//         {
//             for (int x = -worldSizeX; x < worldSizeX; x++)
//             {
//                 for (int z = -worldSizeZ; z < worldSizeZ; z++)
//                 {
//                     Vector3 pos = new Vector3(x * 1 + xPlayerLocation, GenerateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight, z * 1 + zPlayerLocation);

//                     if (!blockContainer.ContainsKey(pos))
//                     {
//                         //GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;
//                         GameObject block = PoolManager.Instance.Pop(blockGameObject);
//                         block.transform.position = pos;
//                         block.transform.rotation = Quaternion.identity;

//                         blockContainer.Add(pos, block);

//                         blockPositions.Add(block.transform.position);

//                         //block.transform.SetParent(this.transform);

//                     }
//                 }
//             }
//         }
//     }

//     private int xPlayerLocation
//     {
//         get
//         {
//             return (int)Mathf.Floor(player.transform.position.x);
//         }
//     }
//     private int zPlayerLocation
//     {
//         get
//         {
//             return (int)Mathf.Floor(player.transform.position.z);
//         }
//     }


//     private int xPlayerMove
//     {
//         get
//         {
//             return (int)(player.transform.position.x - startPosition.x);
//         }
//     }


//     private int zPlayerMove
//     {
//         get
//         {
//             return (int)(player.transform.position.z - startPosition.z);
//         }
//     }


//     private void SpawnObject()
//     {
//         for (int c = 0; c < 20; c++)
//         {
//             GameObject toPlaceObject = PoolManager.Instance.Pop(objectToSpawn);
//             toPlaceObject.transform.position = ObjectSpawnLocation();
//             toPlaceObject.transform.rotation = Quaternion.identity;

//         }
//     }

//     private Vector3 ObjectSpawnLocation()
//     {
//         int rndIndex = Random.Range(0, blockPositions.Count);
//         Vector3 newPos = new Vector3(
//             blockPositions[rndIndex].x,
//             blockPositions[rndIndex].y + 0.5f,
//             blockPositions[rndIndex].z
//         );

//         blockPositions.RemoveAt(rndIndex);
//         return newPos;

//     }

//     private float GenerateNoise(int x, int z, float detailScale)
//     {
//         float xNoise = (x + this.transform.position.x) / detailScale;
//         float zNoise = (z + this.transform.position.y) / detailScale;

//         return Mathf.PerlinNoise(xNoise, zNoise);
//     }
// }
