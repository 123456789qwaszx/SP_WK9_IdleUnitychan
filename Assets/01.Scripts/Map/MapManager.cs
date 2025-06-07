using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapTile : IComparable<MapTile>
{
    public int distance { get; set; }

    public int CompareTo(MapTile other)
    {
        if (distance == other.distance)
            return 0;
        return distance > other.distance ? 1 : -1;
    }
}

public class MapManager : MonoBehaviour
{
    PriorityQueue<MapTile> q = new PriorityQueue<MapTile>();

    // int aroundChunkNum;
    // float width;
    // float length;

    void Start()
    {
        q.Push(new MapTile() { distance = 05 });
        q.Push(new MapTile() { distance = 15 });
        q.Push(new MapTile() { distance = 35 });
        q.Push(new MapTile() { distance = 55 });
        q.Push(new MapTile() { distance = 25 });
    }

    void Update()
    {
        while (q.Count() > 0)
        {
            Debug.Log(q.Pop().distance);
        }


        // for (int x = -aroundChunkNum; x <= aroundChunkNum; x++)
        // {
        //     for (int z = -aroundChunkNum; z <= aroundChunkNum; x++)
        //     {
        //         Vector3 pos = new Vector3(width * x, 0, length * z);
        //     }
        // }
    }
}
