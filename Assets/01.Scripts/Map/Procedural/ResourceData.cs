using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,
    metalOre,
    rockOre,
    tree,
    mushroom
}

[Serializable]
public class ItemDataResourceHp
{
    public ResourceType type;
    public int value;
}

public class ResourceData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public ResourceType resourcetype;
    public GameObject dropPrefab;

    [Header("stacking")]
    public bool canStack;
    public int maxStackAmount;
    


    [Header("Resource")]
    public ItemDataResourceHp[] resources;
}
