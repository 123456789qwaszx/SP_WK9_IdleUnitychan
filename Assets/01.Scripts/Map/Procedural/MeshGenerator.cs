using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
//매쉬 생성
0. Mesh mesh = new Mesh();

// 버택스 생성
1. vertices[] = new Vector3[4];

// 버택스 위치 잡아주고
2. Vector3 p0, p1, p2,p3,p4,p5,p6,p7
    p0 = new Vector3(-0.5f, -0.5f, 0.5f);
    p1 = new Vector3(0.5f, -0.5f, 0.5f);
    p2 = new Vector3(0.5f, -0.5f, -0.5f);
    p3 = new Vector3(-0.5f, -0.5f, -0.5f);
    p4 = new Vector3(-0.5f, 0.5f, 0.5f);
    p5 = new Vector3(0.5f, 0.5f, 0.5f);
    p6 = new Vector3(0.5f, 0.5f, -0.5f);
    p7 = new Vector3(-0.5f, 0.5f, -0.5f);
...
혹은 vertices[i] = new Vector3(x, Mathf.PerlinNoise(x * 2, z * .3f) / .3f, z);

// 처음에 생성한 매쉬의 버택스를, 내가 만든 버택스로 대체
mesh.vertices = vertices;

// Vector3로 꼭지점의 좌표를 배열로 만들고,
// int형 배열을 이용해 꼭지점의 인덱스를 입력해준다.
// 단순한 숫자 같지만, 이 숫자는 vertices에 순서대로 대입한 정점들의 순서. 0 = p6, 1 = p7, 2 = p3, 3 = p2...

// 또 당연히 같은 숫자라도 vertices의 값에 따라 out이 달라지기때문에,
// 반드시 vertices 다음에 위치해야한다.
// 마찬가지로 mesh.triangles도 mesh.verticies 다음에 위치해야하고.
3. int[] triangles = new int[6];
triangles = new int[] { 3, 1, 0, 3, 2, 1 };
...
mesh.triangles = triangles;

혹은 int tris = 0; int verts = 0;
        for (int z = 0; z < Worldz; z++)
        {
            for (int x = 0; x < Worldx; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + Worldz + 1;
                triangles[tris + 2] = verts + 1;

                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + Worldz + 1;
                triangles[tris + 5] = verts + Worldz + 2;

                verts++;
                tris += 6;
            }
            verts++;
        }

//정점에서 메쉬의 경계볼륨을 재계산하는 함수.
4. mesh.RecalculateNormals();
        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

// 위까지가 Mesh를 만든것.
// 아래는 UV(texture)를 효율적으로 입히는 방법.
// 그냥 monobehaviour을 직접 상속받고 드래그앤드랍식으로 붙여도 괜찮지만, 어차피 텍스처아틀라스사용하면서 코드쓸거니 이렇게 분리.
5. monobehavior를 상속받은 것으로부터, material을 받아오고
Vector2[] uvs = new Vector2[4];
Vector2 uv00, uv10, uv01, uv11;
Vector2[,] blockUVs ={new Vector2(0.2125f, 0.9875f)  , new Vector2(0.2250f, 0.9875f);
}

uv00 = blockUVs[0, 0];
uv10 = blockUVs[0, 1];
uv01 = blockUVs[1, 0];
uv11 = blockUVs[1, 1];

uvs = new Vector2[] { uv11, uv01, uv00, uv10 };
mesh.uv = uvs;


    --- Optional Enhancers ---
    ToDo: Implement noise smoothing (Theres a tutorial for this on my channel ;)
    --------------------------
*/
public class MeshGenerator : MonoBehaviour
{
    public int Worldx;
    public int Worldz;
        
    private Vector3[] vertices;
    private int[] triangles;

    private MeshCollider GetMeshCollider
    {
        get
        {
            return GetComponent<MeshCollider>();
        }
    }

    private MeshFilter GetMeshFilter
    {
        get
        {
            return GetComponent<MeshFilter>();
        }
    }

    void Start()
    {
        generateMesh();
    }

    // Method that does our mesh stuff :)
    private void generateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "ProceduralMesh";

        mesh.Clear();

        triangles = new int[Worldx * Worldz * 6];
        vertices = new Vector3[(Worldx + 1) * (Worldz + 1)];

        for (int i = 0, z = 0; z <= Worldz; z++)
        {
            for (int x = 0; x <= Worldx; x++)
            {
                vertices[i] = new Vector3(x, Mathf.PerlinNoise(x * 2, z * .3f) / .3f, z);
                i++;
            }
        }

        int tris = 0;
        int verts = 0;

        for (int z = 0; z < Worldz; z++)
        {
            for (int x = 0; x < Worldx; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + Worldz + 1;
                triangles[tris + 2] = verts + 1;

                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + Worldz + 1;
                triangles[tris + 5] = verts + Worldz + 2;

                verts++;
                tris += 6;
            }
            verts++;
        }


        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GetMeshFilter.mesh = mesh;
        GetMeshCollider.sharedMesh = mesh;

    }
}