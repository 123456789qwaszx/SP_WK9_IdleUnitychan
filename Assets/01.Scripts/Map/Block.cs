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
        /* GRASS TOP */                         {new Vector2(0.2125f, 0.9875f)  , new Vector2(0.2250f, 0.9875f) , new Vector2(0.2125f, 1f)  , new Vector2(0.2250f, 1f)},
        /* GRASS SIDE */                        {new Vector2(0.2250f, 0.9875f)  , new Vector2(0.2375f, 0.9875f) , new Vector2(0.2250f, 1f)  , new Vector2(0.2375f, 1f)},
        /* DIRT */                              {new Vector2(0.2000f, 0.9875f)  , new Vector2(0.2125f, 0.9875f) , new Vector2(0.2000f, 1f)  , new Vector2(0.2125f, 1f)},
    };

    public Block(BlockType b, Vector3 pos, GameObject p, Chunk o)
    {
        bType = b;
        owner = o;
        parent = p;
        position = pos;
        if (bType == BlockType.AIR) isSolid = false;
        else isSolid = true;
    }

    // ChunkSize에 맞게 Local좌표를 계산하는 함수
    int ConvertBlockIndexToLocal(int i)
    {
        if (i == -1) i = World.chunkSize - 1;
        else if (i == World.chunkSize) i = 0;
        return i;
    }


    // 1. 우선적으로 그려야 하는 해당 쿼드의 Local좌표가 다른 Chunk에 있는지 없는지 확인.
    // 2. 만약 Local 좌표가 chunksize의 값보다 크거나 작다면(해당 청크의 Local좌표를 벗어난경우), 그에 맞는 해당블럭의 Chunk의 위치를 찾고,
    // TryGetValue함수를 이용해 Chunk의 블럭 정보를 가져오는데, 해당 청크가 없다면 false를 반환.
    // 3. try/catch를 이용해 그려야 하는 해당 블럭의 isSolid를 반환. (고체일 경우 true)
    // 해당 블럭이 고체인지 판단하는 함수
    public bool HasSolidNeighbour(int x, int y, int z)
    {
        Block[,,] chunks;

        // ChunkSize를 벗어날 경우
        if (x < 0 || x >= World.chunkSize || y < 0 || y >= World.chunkSize || z < 0 || z >= World.chunkSize)
        {
            // 해당 블럭에 맞는 다른 청크의 위치를 구함
            Vector3 neightbourChunkPos = this.parent.transform.position + new Vector3((x - (int)position.x) * World.chunkSize,
                (y - (int)position.y) * World.chunkSize, (z - (int)position.z) * World.chunkSize);

            // 새로 구한 청크의 위치값을 이용해 청크의 이름을 구함
            string nName = World.BuildChunkName(neightbourChunkPos);

            // 새로운 청크에 맞는 로컬좌표 값 계산
            x = ConvertBlockIndexToLocal(x);
            y = ConvertBlockIndexToLocal(y);
            z = ConvertBlockIndexToLocal(z);

            Chunk nChunk;

            // 값 계산으로 구한 청크의 이름으로 Chunk변수를 구함.
            if (World.chunks.TryGetValue(nName, out nChunk))
            {
                // 해당 청크의 블럭 값을 대입
                chunks = nChunk.chunkData;
            }
            else return false;
        } // 아닐경우 기존 청크의 블럭 값을 대입
        else chunks = owner.chunkData;

        try
        {
            // 위치에 맞는 블럭의 고체 여부 확인
            return chunks[x, y, z].isSolid;
        }
        catch (System.IndexOutOfRangeException ex) { Debug.Log(ex); }

        return false;
    }

    // public void Draw()
    // {
    //     if (bType.Equals(BlockType.AIR)) return;

    //     CreateQuad(Cubeside.BOTTOM);
    //     CreateQuad(Cubeside.TOP);
    //     CreateQuad(Cubeside.LEFT);
    //     CreateQuad(Cubeside.RIGHT);
    //     CreateQuad(Cubeside.FRONT);
    //     CreateQuad(Cubeside.BACK);
    // }

    public void Draw()
    {
        if (bType.Equals(BlockType.AIR)) return;

        // 해당 면의 옆 블럭이 고체가 아닐경우 Quad생성
        if (!HasSolidNeighbour((int)position.x, (int)position.y, (int)position.z + 1))
            CreateQuad(Cubeside.FRONT);
        if (!HasSolidNeighbour((int)position.x, (int)position.y, (int)position.z - 1))
            CreateQuad(Cubeside.BACK);
        if (!HasSolidNeighbour((int)position.x, (int)position.y + 1, (int)position.z))
            CreateQuad(Cubeside.TOP);
        if (!HasSolidNeighbour((int)position.x, (int)position.y - 1, (int)position.z))
            CreateQuad(Cubeside.BOTTOM);
        if (!HasSolidNeighbour((int)position.x - 1, (int)position.y, (int)position.z))
            CreateQuad(Cubeside.LEFT);
        if (!HasSolidNeighbour((int)position.x + 1, (int)position.y, (int)position.z))
            CreateQuad(Cubeside.RIGHT);
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
        } // BOTTOM을 그릴 경우
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