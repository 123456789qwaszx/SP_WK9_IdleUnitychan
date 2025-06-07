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
    void Update()
    {
        PriorityQueue<MapTile> q = new PriorityQueue<MapTile>();
        q.Push(new MapTile() { distance = 05 });
        q.Push(new MapTile() { distance = 15 });
        q.Push(new MapTile() { distance = 35 });
        q.Push(new MapTile() { distance = 55 });
        q.Push(new MapTile() { distance = 25 });

        while (q.Count() > 0)
        {
            Debug.Log(q.Pop().distance);
        }
    }
}
