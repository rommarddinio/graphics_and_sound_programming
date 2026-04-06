using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerResult
{
    public string playerName;
    public float score;
}

[System.Serializable]
public class Results
{
    public List<PlayerResult> results = new List<PlayerResult>();
}

public class DataManager : MonoBehaviour
{
    private string playerName;
    private string fileName = "results.json";
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void SaveResult(float score)
    {
        Results raceResults = LoadResults();

        PlayerResult existing = raceResults.results.Find(r => r.playerName == playerName);
        if (existing != null)
        {
            existing.score = score;
        }
        else
        {
            raceResults.results.Add(new PlayerResult { playerName = playerName, score = score });
        }

        string json = JsonUtility.ToJson(raceResults, true);
        File.WriteAllText(filePath, json);
    }

    public Results LoadResults()
    {
        if (!File.Exists(filePath))
        {
            return new Results();
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Results>(json);
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public void FillScrollView(Transform content, GameObject itemPrefab)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        Results results = LoadResults();

        results.results.Sort((a, b) => b.score.CompareTo(a.score));

        int place = 1;

        foreach (var r in results.results)
        {
            GameObject item = Instantiate(itemPrefab, content);

            TMP_Text[] texts = item.GetComponentsInChildren<TMP_Text>();

            if (texts.Length >= 3)
            {
                texts[0].text = place.ToString();
                texts[1].text = r.playerName;       
                texts[2].text = r.score.ToString();   
            }

            place++;
        }
    }
}