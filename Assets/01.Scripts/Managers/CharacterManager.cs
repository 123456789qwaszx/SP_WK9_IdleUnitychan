using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    // 시작 시 Player를 포함한 Char들에게서 직접 데이터를 받는다.
    // 외부에서 Char들의 정보를 받고 싶을때, Char들의 연결통로 역할
    // Manager.Char.Player.PlayerCondition.value...

    // 문제는 컨디션은?
    // 그리고 UI... 변수를 UI에서 선언하고, 그걸 Player에서 받아오는 게 맞나?
    // 절대 아니야

    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
}
