// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class World : MonoBehaviour
// {
//     public static int chunkSize = 8;
//     public static GameObject ChunksObject;
//     public Material cube_material;

//     public static Dictionary<string, Chunk> chunks;

//     // 위치값을 기반으로 청크 이름 생성
//     public static string BuildChunkName(Vector3 v)
//     {
//         return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
//     }

//     // 청크들을 생성하는 함수
//     IEnumerator BuildWorld()
//     {
//         // 청크 생성
//         CreateChunk(0, 0, 0);

//         // Dictionary안에 있는 모든 Chunk 그리기.
//         foreach (KeyValuePair<string, Chunk> c in chunks)
//         {
//             c.Value.DrawChunk();
//         }

//         yield return null;
//     }

//     void Start()
//     {
//         ChunksObject = this.gameObject;
//         chunks = new Dictionary<string, Chunk>();
//         this.transform.position = Vector3.zero;
//         this.transform.rotation = Quaternion.identity;
//         StartCoroutine(BuildWorld());
//     }

//     // 청크를 생성하는 함수
//     public static void CreateChunk(int x, int y, int z)
//     {
//         // 새로운 청크를 생성
//         Chunk c = new Chunk(new Vector3(x, y, z));

//         // 생성한 청크를 자식 오브젝트로 설정
//         c.chunk.transform.SetParent(ChunksObject.transform);

//         // Dictionary에 값 추가
//         chunks.Add(c.chunk.name, c);
//     }

// }