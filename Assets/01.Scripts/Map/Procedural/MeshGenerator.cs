using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
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