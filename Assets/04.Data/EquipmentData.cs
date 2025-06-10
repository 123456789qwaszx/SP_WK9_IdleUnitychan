using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    Weapon,
    Armor,
}

[Serializable]
public class EquipValue
{
    public EquipType type;
    public int value;
}

[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    [Header("Info")]
    public string equipmentName;
    public EquipType equipmentType;
    public GameObject equipPrefab;

    [Header("EnemyStat")]
    public EquipValue[] equipmentValue;
}
