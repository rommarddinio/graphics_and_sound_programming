using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerResult
{
    public string playerName;
    public float time;
}

[System.Serializable]
public class RaceResults
{
    public List<PlayerResult> results = new List<PlayerResult>();
}

public class RaceDataManager : MonoBehaviour
{
    private string playerName;
    private string fileName = "raceResults.json";
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void setName(string name)
    {
        this.playerName = name;
    }
    public void SaveResult(float time)
    {
        RaceResults raceResults = LoadResults();

        PlayerResult existing = raceResults.results.Find(r => r.playerName == playerName);
        if (existing != null)
        {
            existing.time = time; 
        }
        else
        {
            raceResults.results.Add(new PlayerResult { playerName = playerName, time = time });
        }

        string json = JsonUtility.ToJson(raceResults, true);
        File.WriteAllText(filePath, json);
    }

    public RaceResults LoadResults()
    {
        if (!File.Exists(filePath))
        {
            return new RaceResults();
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<RaceResults>(json);
    }
}