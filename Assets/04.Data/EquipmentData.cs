using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
}

[Serializable]
public class EquipmentStatus
{
    public EquipmentType type;
    public int value;
}

[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    [Header("Info")]
    public string equipmentName;
    public string description;
    public EquipmentType equipmentType;
    public GameObject dropPrefab;
    public GameObject equipPrefab;
    public Sprite equipmentIcon;

    [Header("stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("EquipmenyStatus")]
    public EquipmentStatus[] equipmentStatus;
}
