// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Chunktest
// {
//     public Material cube_material;
//     public Block[,,] chunkData;
//     public GameObject chunk;

//     public Chunk(Vector3 position)
//     {
//         chunk = new GameObject(World.BuildChunkName(position));
//         chunk.transform.position = position;
//         cube_material = World.ChunksObject.GetComponent<World>().cube_material;
//         BuildChunk();
//     }


//     // 청크의 값을 생성하는 함수.
//     void BuildChunk()
//     {
//         // ChunkSize의 사이즈로 초기화
//         chunkData = new Block[World.chunkSize, World.chunkSize, World.chunkSize];

//         // 3중 for문을 이용하여 3차원 좌표를 표현
//         for(int z = 0; z < World.chunkSize; z++)
//         {
//             for(int x = 0; x < World.chunkSize; x++)
//             {
//                 for(int y = 0; y < World.chunkSize; y++)
//                 {
//                     Vector3 pos = new Vector3(x, y, z);
//                     // 위치가 0,0,0인 청크의 바닥만 GRASS로 생성
//                     if (pos.y == 0 && chunk.transform.position.y == 0 && chunk.transform.position == Vector3.zero)
//                     {
//                         chunkData[x, y, z] = new Block(Block.BlockType.GRASS, pos, chunk, this);
//                     } // 그 외의 모든 것은 AIR로 생성
//                     else chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, chunk, this);
//                 }
//             }
//         }
//     }

//     // 청크의 값을 기반으로 오브젝트를 그리는 함수
//     public void DrawChunk()
//     {
//         // 3중 for문을 이용하여 3차원 좌표를 표현
//         for (int z = 0; z < World.chunkSize; z++)
//         {
//             for(int x = 0; x < World.chunkSize; x++)
//             {
//                 for(int y = 0; y < World.chunkSize; y++)
//                 {
//                     // 각 블럭을 그리기
//                     chunkData[x, y, z].Draw();
//                 }
//             }
//         }

//         // 자식 오브젝트의 메쉬를 하나의 오브젝트로 합치기
//         CombineQuads(chunk, cube_material);

//         MeshCollider meshCollider = chunk.gameObject.GetComponent<MeshCollider>();

//         // MeshCollider가 없을 경우
//         if (meshCollider == null)
//         {
//             // MeshCollider컴포넌트 추가
//             meshCollider = chunk.AddComponent<MeshCollider>();
//             // meshCollider의 Mesh에 MeshFilter의 Mesh 대입
//             meshCollider.sharedMesh = chunk.GetComponent<MeshFilter>().mesh;
//         } // 아닐경우
//         else
//         {
//             // 변경한 Mesh의 맞게 MeshCollider 새로고침
//             meshCollider.sharedMesh = chunk.GetComponent<MeshFilter>().mesh;
//         }
//     }


//     // 자식 오브젝트의 메쉬를 하나의 오브젝트로 합치는 함수
//     public void CombineQuads(GameObject chunk, Material cube_material)
//     {
//         //1. 모든 자식 메쉬 결합
//         MeshFilter[] meshFilters = chunk.GetComponentsInChildren<MeshFilter>();
//         CombineInstance[] combine = new CombineInstance[meshFilters.Length];
//         int i = 0;
//         while (i < meshFilters.Length)
//         {
//             combine[i].mesh = meshFilters[i].sharedMesh;
//             combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
//             i++;
//         }

//         MeshFilter meshFilter = chunk.gameObject.GetComponent<MeshFilter>();

//         if (meshFilter == null)
//         {
//             //2. 부모 오브젝트에 새로운 Mesh만들기
//             MeshFilter mf = chunk.gameObject.AddComponent<MeshFilter>();
//             mf.mesh = new Mesh();

//             //3. 자식 오브젝트의 결합된 Mesh를 부모 오브젝트의 Mesh로 추가
//             mf.mesh.CombineMeshes(combine);

//             //4. 부모 오브젝트의 MeshRendere 만들기
//             MeshRenderer renderer = chunk.gameObject.AddComponent<MeshRenderer>();
//             renderer.material = cube_material;
//         }
//         else
//         {
//             //3. 자식 오브젝트의 결합된 Mesh를 부모 오브젝트의 Mesh로 추가
//             meshFilter.mesh.Clear();
//             meshFilter.mesh = new Mesh();
//             meshFilter.mesh.CombineMeshes(combine);
//         }

//         //5. 결합되지 않은 모든 하위 항목 삭제
//         foreach (Transform quad in chunk.transform)
//         {
//             GameObject.Destroy(quad.gameObject);
//         }

//         return;
//     }
// }