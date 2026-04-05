using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    [Header("Панели")]
    public GameObject MainMenuPanel;
    public GameObject RecordsPanel;
    public GameObject GameModePanel;
    public GameObject DeathPanel;

    [Header("Кнопки")]
    public Button StartButton;
    public Button ExitButton;
    public Button RestartButton;
    public Button RecordsButton;
    public Button SaveButton;
    public Button BackButton;

    [Header("Счётчик")]
    public TMP_Text text;
    private int count = 0;

    public TMP_Text result;

    public Character player;

    void Awake()
    {
        if (StartButton != null)
            StartButton.onClick.AddListener(OnStartClicked);
        if (ExitButton != null)
            ExitButton.onClick.AddListener(OnExitClicked);
        if (RestartButton != null)
            RestartButton.onClick.AddListener(OnRestartClicked);
        if (RecordsButton != null)
            RecordsButton.onClick.AddListener(OnRecordsClicked);
        if (SaveButton != null)
            SaveButton.onClick.AddListener(OnSaveClicked);
        if (BackButton != null) { 
            BackButton.onClick.AddListener(OnBackToMainClicked);
            BackButton.gameObject.SetActive(false);
        }

        ShowPanel(MainMenuPanel);
    }

    void ShowPanel(GameObject panelToShow)
    {
        MainMenuPanel.SetActive(false);
        RecordsPanel.SetActive(false);
        GameModePanel.SetActive(false);
        DeathPanel.SetActive(false);

        if (panelToShow != null)
            panelToShow.SetActive(true);
    }

    public void OnStartClicked()
    {
        text.text = "0";
        text.gameObject.SetActive(true);
        ShowPanel(null); 
        if (player != null)
            player.StartGame(); 
    }

    public void OnExitClicked()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
    #else
        Application.Quit(); 
    #endif
    }

    public void OnRestartClicked()
    {
        Debug.Log("Restart Game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnRecordsClicked()
    {
        ShowPanel(RecordsPanel);
    }

    public void OnGameModeClicked()
    {
        ShowPanel(GameModePanel);
    }

    public void OnBackToMainClicked()
    {
        ShowPanel(MainMenuPanel);
    }

    public void OnSaveClicked()
    {

    }

    public void ShowDeathPanel()
    {
        text.gameObject.SetActive(false);
        result.text += "Ваш результат: " + text.text;
        ShowPanel(DeathPanel);
    }

    public void IncreaseCount()
    {
        count++;
        if (text != null)
            text.text = count.ToString();
    }

}