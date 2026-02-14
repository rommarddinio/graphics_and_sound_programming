using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using NUnit.Framework;

public class RaceResult : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text resultText2;
    [SerializeField] private RaceDataManager raceDataManager;


    private void Start()
    {
        resultPanel.SetActive(false);
    }

    public void Finish(float raceTime)
    {
        raceDataManager.SaveResult(raceTime);
        RaceResults wrap = raceDataManager.LoadResults();
        wrap.results.Sort((a, b) => a.time.CompareTo(b.time));
        if (wrap != null)
        {
            string text = "";
            foreach (var res in wrap.results)
            {
                text += res.playerName + ":" + res.time + "\n";
            }
            resultText2.text = text;
        }
        resultPanel.SetActive(true);
        resultText.text = "Your time: " + raceTime.ToString("F2") + " sec";

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
