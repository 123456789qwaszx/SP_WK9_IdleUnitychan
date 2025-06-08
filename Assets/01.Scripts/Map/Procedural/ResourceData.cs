using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,
    tree,
    stoneOre,
    metalOre,
    mushroom,
    fish
}

[Serializable]
public class ResourceHp
{
    public ResourceType type;
    public int value;
}


[CreateAssetMenu(fileName = "Resource", menuName = "New Resource")]
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
    public ResourceHp[] resources;
}
