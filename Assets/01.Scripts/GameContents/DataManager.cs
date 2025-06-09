using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System;

public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class StatData
{
    public List<Stat> stats = new List<Stat>();
}


public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();



    public void Init()
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/StatData");
        StatData data = JsonUtility.FromJson<StatData>(textAsset.text);

        foreach (Stat stat in data.stats)
            StatDict.Add(stat.level, stat);
    }


    // string path = Path.Combine(Application.streamingAssetsPath, "TestStatData.json");

    // void Start()
    // {
    //     Show1();
    // }

    // void Show1()
    // {
    //     string json = File.ReadAllText(path);
    //     Debug.Log(json);
    //     JToken root = JToken.Parse(json);

    //     JToken players = root["players"];


    //     for (int i = 0; i < players.Count(); i++)
    //     {
    //         Debug.Log($"{players[i]["id"]} 이 플레이어 번호고" + $"이름은 {players[i]["name"]}");
    //         JToken skills = players[i]["skills"];

    //         for (int j = 0; j < skills.Count(); j++)
    //         {
    //             JToken skill = skills[j];
    //             Debug.Log($"{skill["damage"]}");
    //         }
    //     }

    //     Debug.Log($"{players[0]["skills"][0]["effects"][1]["duration"]}");
    // }
}
