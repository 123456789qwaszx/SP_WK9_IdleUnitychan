// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BlockTest : MonoBehaviour
// {
//     public enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK };

//     public GameObject parent;
//     public Vector3 position;
//     public Material m;

//     public BlockTest(Vector3 pos, GameObject p)
//     {
//         parent = p;
//         position = pos;
//     }

//     private void Start()
//     {
//         Draw();
//     }

//     void Draw()
//     {
//         CreateQuad(Cubeside.BOTTOM);
//         CreateQuad(Cubeside.TOP);
//         CreateQuad(Cubeside.LEFT);
//         CreateQuad(Cubeside.RIGHT);
//         CreateQuad(Cubeside.FRONT);
//         CreateQuad(Cubeside.BACK);
//     }

//     void CreateQuad(Cubeside side)
//     {
//         Mesh mesh = new Mesh();
//         mesh.name = "ScriptedMesh";

//         Vector2[] uvs = new Vector2[4];
//         Vector3[] vertices = new Vector3[4];
//         int[] triangles = new int[6];

//         Vector2 uv00, uv10, uv01, uv11;

//         // // 이부분을 수정함으로 써 TextAtlas에서 일부분 잘라 사용할 수 있다.
//         // uv00 = new Vector2(0, 0);
//         // uv10 = new Vector2(1, 0);
//         // uv01 = new Vector2(0, 1);
//         // uv11 = new Vector2(1, 1);
//         uv11 = new Vector2(0.2125f, 0.9875f);
//         uv00 = new Vector2(0.2125f, 0.9875f);
//         uv10 = new Vector2(0.2125f, 0.9875f);
//         uv01 = new Vector2(0.2125f, 0.9875f);

//         Vector3 p0, p1, p2, p3, p4, p5, p6, p7;

//         p0 = new Vector3(-0.5f, -0.5f, 0.5f);
//         p1 = new Vector3(0.5f, -0.5f, 0.5f);
//         p2 = new Vector3(0.5f, -0.5f, -0.5f);
//         p3 = new Vector3(-0.5f, -0.5f, -0.5f);
//         p4 = new Vector3(-0.5f, 0.5f, 0.5f);
//         p5 = new Vector3(0.5f, 0.5f, 0.5f);
//         p6 = new Vector3(0.5f, 0.5f, -0.5f);
//         p7 = new Vector3(-0.5f, 0.5f, -0.5f);

//         switch (side)
//         {
//             case Cubeside.BOTTOM:
//                 vertices = new Vector3[] { p0, p1, p2, p3 };
//                 break;
//             case Cubeside.TOP:
//                 vertices = new Vector3[] { p7, p6, p5, p4 };
//                 break;
//             case Cubeside.LEFT:
//                 vertices = new Vector3[] { p7, p4, p0, p3 };
//                 break;
//             case Cubeside.RIGHT:
//                 vertices = new Vector3[] { p5, p6, p2, p1 };
//                 break;
//             case Cubeside.FRONT:
//                 vertices = new Vector3[] { p4, p5, p1, p0 };
//                 break;
//             case Cubeside.BACK:
//                 vertices = new Vector3[] { p6, p7, p3, p2 };
//                 break;
//         }

//         uvs = new Vector2[] { uv11, uv01, uv00, uv10 };
//         triangles = new int[] { 3, 1, 0, 3, 2, 1 };

//         mesh.vertices = vertices;
//         mesh.uv = uvs;
//         mesh.triangles = triangles;

//         mesh.RecalculateBounds();
//         mesh.RecalculateNormals();

//         GameObject quad = new GameObject("Quad");

//         MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
//         meshFilter.mesh = mesh;

//         MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
//         meshRenderer.material = m;
//     }
// }