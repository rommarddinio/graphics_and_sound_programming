using UnityEngine;
using TMPro;

public class RecordsUI : MonoBehaviour
{
    [Header("ScrollView")]
    public RectTransform content;       
    public GameObject recordItemPrefab;
    public DataManager dataManager;

    public float itemHeight = 40f;

    public void FillScrollView()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        Results results = dataManager.LoadResults();
        results.results.Sort((a, b) => b.score.CompareTo(a.score));

        int rank = 1;

        foreach (PlayerResult r in results.results)
        {
            GameObject item = Instantiate(recordItemPrefab, content);

            TMP_Text rankText = item.transform.Find("Place")?.GetComponent<TMP_Text>();
            TMP_Text nameText = item.transform.Find("Name")?.GetComponent<TMP_Text>();
            TMP_Text scoreText = item.transform.Find("Score")?.GetComponent<TMP_Text>();

            if (rankText != null)
                rankText.text = rank.ToString();
            if (nameText != null)
                nameText.text = r.playerName;
            if (scoreText != null)
                scoreText.text = r.score.ToString();

            rank++;
        }

        float newHeight = results.results.Count * itemHeight;
        content.sizeDelta = new Vector2(content.sizeDelta.x, newHeight);
    }
}