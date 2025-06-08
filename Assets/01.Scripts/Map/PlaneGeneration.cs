using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{

    public GameObject plane;

    public GameObject player;

    private int radius = 5;

    private int planeOffset = 10;


    private Vector3 startPos = Vector3.zero;

    private int xPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int zPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    private Dictionary<string, Vector3> tilePlane = new Dictionary<string, Vector3>();

    void Update()
    {   
        // 딱 시작할때만 동작. 즉 start랑 똑같음.
        if (startPos == Vector3.zero)
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * planeOffset + XPlayerLocation,0, z * planeOffset + ZPlayerLocation);

                    if (!tilePlane.ContainsValue(pos))
                    {
                        GameObject _plane = PoolManager.Instance.Pop(plane);
                        _plane.transform.position = pos;
                        _plane.transform.rotation = Quaternion.identity;

                        tilePlane.Add($"{pos}", _plane.transform.position);
                    }
                }
            }
        }

        if (hasPlayerMoved())
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * planeOffset + XPlayerLocation,0, z * planeOffset + ZPlayerLocation);

                    if (!tilePlane.ContainsValue(pos))
                    {
                        GameObject _plane = PoolManager.Instance.Pop(plane);
                        _plane.transform.position = pos;
                        _plane.transform.rotation = Quaternion.identity;

                        tilePlane.Add($"{pos}", _plane.transform.position);
                    }
                }
            }
        }
    }

    bool hasPlayerMoved()
    {
        if (Mathf.Abs(xPlayerMove) >= planeOffset || Mathf.Abs(zPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }
}
