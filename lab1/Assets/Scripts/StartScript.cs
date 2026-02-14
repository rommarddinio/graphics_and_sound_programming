using UnityEngine;
using TMPro;
public class StartScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField text;
    [SerializeField] private RaceDataManager dataManager;
    [SerializeField] private Countdown countdown;
    void Start()
    {
        panel.SetActive(true);
    }

    public void GetInfo()
    {
        dataManager.setName(text.text);
        panel.SetActive(false);
        countdown.setReady(true);
    }
}
