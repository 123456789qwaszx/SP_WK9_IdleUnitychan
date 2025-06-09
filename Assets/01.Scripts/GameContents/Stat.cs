using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    int _level;
    [SerializeField]
    int _hp;
    [SerializeField]
    int _maxHp;
    [SerializeField]
    int _attack;
    [SerializeField]
    int _defense;
    [SerializeField]
    float _moveSpeed;

    public int Level { get { return _level; } set{ _level = value; }}
    public int Hp { get { return _hp; } set{ _hp = value; }}
    public int MaxHp { get { return _maxHp; } set{ _maxHp = value; }}
    public int Attack { get { return _attack; } set{ _attack = value; }}
    public int Defense { get { return _defense; } set{ _defense = value; }}
    public float MoveSpeed { get { return _moveSpeed; } set{ _moveSpeed = value; }}

}
