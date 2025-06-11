using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    void Start()
    {
        Dictionary<int, Data.Stat> dict = DataManager.Instance.StatDict;
        // Debug.Log(dict[1]);
        // Debug.Log(dict[0]);
        // Debug.Log(dict[2]);
    }

}
