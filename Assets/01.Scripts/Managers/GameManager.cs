using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Vector2 MouseDir { get; set; } = Vector2.zero;

    public bool JumpInput;
}
