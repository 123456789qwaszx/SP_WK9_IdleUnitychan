using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK };

    public enum BlockType
    {
        AIR, GRASS, DIRT
    };

    public BlockType bType;

    public bool isSolid;
    public Chunk owner;
    public GameObject parent;
    public Vector3 position;

    // 블럭에 따른 UV들
    Vector2[,] blockUVs =
    {
        {new Vector2(0.2125f, 0.9875f)  , new Vector2(0.2250f, 0.9875f) , new Vector2(0.2125f, 1f)  , new Vector2(0.2250f, 1f)}, // GRASS TOP
        {new Vector2(0.2250f, 0.9875f)  , new Vector2(0.2375f, 0.9875f) , new Vector2(0.2250f, 1f)  , new Vector2(0.2375f, 1f)}, // GRASS SIDE
        {new Vector2(0.2000f, 0.9875f)  , new Vector2(0.2125f, 0.9875f) , new Vector2(0.2000f, 1f)  , new Vector2(0.2125f, 1f)}, // DIRT
    };

    public Block(BlockType b, Vector3 pos, GameObject p, Material m)
    {
        bType = b;
        parent = p;
        position = pos;
        if (bType == BlockType.AIR) isSolid = false;
        else isSolid = true;
    }

    // ChunkSize에 맞게 Local좌표를 계산하는 함수
    int ConvertBlockIndexToLocal(int i)
    {
        if (i == -1) i = owner.chunkSize - 1;
        else if (i == owner.chunkSize) i = 0;
        return i;
    }


    public void Draw()
    {
        
        if (bType.Equals(BlockType.AIR)) return;
        
        CreateQuad(Cubeside.BOTTOM);
        CreateQuad(Cubeside.TOP);
        CreateQuad(Cubeside.LEFT);
        CreateQuad(Cubeside.RIGHT);
        CreateQuad(Cubeside.FRONT);
        CreateQuad(Cubeside.BACK);
    }

    void CreateQuad(Cubeside side)
    {
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";

        Vector2[] uvs = new Vector2[4];
        Vector3[] normals = new Vector3[4];
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        Vector2 uv00, uv10, uv01, uv11;

        Vector3 p0, p1, p2, p3, p4, p5, p6, p7;

        p0 = new Vector3(-0.5f, -0.5f, 0.5f);
        p1 = new Vector3(0.5f, -0.5f, 0.5f);
        p2 = new Vector3(0.5f, -0.5f, -0.5f);
        p3 = new Vector3(-0.5f, -0.5f, -0.5f);
        p4 = new Vector3(-0.5f, 0.5f, 0.5f);
        p5 = new Vector3(0.5f, 0.5f, 0.5f);
        p6 = new Vector3(0.5f, 0.5f, -0.5f);
        p7 = new Vector3(-0.5f, 0.5f, -0.5f);


        // 블럭의 종류가 GRASS이고 Top을 그릴 경우
        if (bType == BlockType.GRASS && side == Cubeside.TOP)
        {
            uv00 = blockUVs[0, 0];
            uv10 = blockUVs[0, 1];
            uv01 = blockUVs[0, 2];
            uv11 = blockUVs[0, 3];
        }
        // BOTTOM을 그릴 경우
        else if (bType == BlockType.GRASS && side == Cubeside.BOTTOM)
        {
            uv00 = blockUVs[(int)(BlockType.DIRT), 0];
            uv10 = blockUVs[(int)(BlockType.DIRT), 1];
            uv01 = blockUVs[(int)(BlockType.DIRT), 2];
            uv11 = blockUVs[(int)(BlockType.DIRT), 3];
        }
        else
        {
            uv00 = blockUVs[(int)(bType), 0];
            uv10 = blockUVs[(int)(bType), 1];
            uv01 = blockUVs[(int)(bType), 2];
            uv11 = blockUVs[(int)(bType), 3];
        }

        switch (side)
        {
            case Cubeside.BOTTOM:
                vertices = new Vector3[] { p0, p1, p2, p3 };
                normals = new Vector3[] { Vector3.down, Vector3.down, Vector3.down, Vector3.down };
                break;
            case Cubeside.TOP:
                vertices = new Vector3[] { p7, p6, p5, p4 };
                normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
                break;
            case Cubeside.LEFT:
                vertices = new Vector3[] { p7, p4, p0, p3 };
                normals = new Vector3[] { Vector3.left, Vector3.left, Vector3.left, Vector3.left };
                break;
            case Cubeside.RIGHT:
                vertices = new Vector3[] { p5, p6, p2, p1 };
                normals = new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right };
                break;
            case Cubeside.FRONT:
                vertices = new Vector3[] { p4, p5, p1, p0 };
                normals = new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward };
                break;
            case Cubeside.BACK:
                vertices = new Vector3[] { p6, p7, p3, p2 };
                normals = new Vector3[] { Vector3.back, Vector3.back, Vector3.back, Vector3.back };
                break;
        }

        uvs = new Vector2[] { uv11, uv01, uv00, uv10 };
        triangles = new int[] { 3, 1, 0, 3, 2, 1 };

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GameObject quad = new GameObject("Quad");
        quad.transform.position = position;
        quad.transform.parent = parent.transform;

        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}