using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    None,
    Bear,
    Skeleton,
    Orc,
}

public enum MonsterStat
{
    None,
    Attack,
    Defense,
    MaxHp,
    MaxLv
}

[Serializable]
public class SetEnemyStat
{
    public MonsterStat type;
    public int value;
}


[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public EnemyType enemytype;

    [Header("EnemyStat")]
    public SetEnemyStat[] monsterStat;
}
