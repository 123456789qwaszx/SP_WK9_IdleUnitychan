using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject resourcePrefab;

    public Transform[] nodes;

    public ResourceType currentResoure;

    void Start()
    {

    }
}

public class NodeManager
{
    private Transform[] nodes;

    public Hashtable nodeDuplicateCheck = new Hashtable();

    public NodeManager(Transform[] node)
    {
        this.nodes = node;
    }

    public int randomNodeSelection
    {
        get
        {
            return Random.Range(0, nodes.Length);

        }
    }

    public int randomResourceSelection
    {
        get
        {
            return Random.Range(0, 100) % 50;
        }
    }

    public bool doesResourceExist(Vector3 pos)
    {
        return nodeDuplicateCheck.ContainsKey(pos);
    }

    public ResourceType returnRandomResourceNode
    {
        get
        {
            if (randomResourceSelection >= 20)
            {
                return ResourceType.metalOre;
            }
            else
            {
                return ResourceType.rockOre;
            }
        }
    }

    
}
