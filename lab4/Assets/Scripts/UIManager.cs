using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public static string lastOpenedPanel = "MainMenu";
    [Header("Панели")]
    public GameObject MainMenuPanel;
    public GameObject RecordsPanel;
    public GameObject GameModePanel;
    public GameObject DeathPanel;

    [Header("Кнопки главного экрана")]
    public Button StartButton;
    public Button ExitButton;
    public Button RestartButton;
    public Button RecordsButton;
    public Button ModeButton;
    public Button SaveButton;
    public Button BackButton;

    [Header("Счётчик")]
    public TMP_Text text;
    private int count = 0;

    public Transform content;
    public TMP_InputField nameField;
    public TMP_Text result;
    public TMP_Text bonusTime;

    public DataManager dataManager;
    public RecordsUI recordsUI;


    void Awake()
    {
        switch (lastOpenedPanel)
        {
            case "GameMode":
                ShowPanel(GameModePanel);
                break;
            default:
                ShowPanel(MainMenuPanel);
                break;
        }
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
        if (ModeButton != null)
            ModeButton.onClick.AddListener(OnGameModeClicked);
        if (BackButton != null) { 
            BackButton.onClick.AddListener(OnBackToMainClicked);
            BackButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (BonusManager.Instance != null && BonusManager.Instance.IsBonusActive())
        {
            bonusTime.gameObject.SetActive(true);

            float timeLeft = BonusManager.Instance.RemainingBonusTime();
            bonusTime.text = "Бонус x2: " + Mathf.Ceil(timeLeft).ToString() + "s";
        }
        else
        {
            bonusTime.gameObject.SetActive(false);
        }
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
        if (PlayerManager.Instance.HasPlayer())
            PlayerManager.Instance.CurrentPlayer.StartGame();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnRecordsClicked()
    {
        BackButton.gameObject.SetActive(true);
        recordsUI.FillScrollView();
        ShowPanel(RecordsPanel);
    }

    public void OnGameModeClicked()
    {
        ShowPanel(GameModePanel);
        lastOpenedPanel = "GameMode";
    }

    public void OnBackToMainClicked()
    {
        BackButton.gameObject.SetActive(false);
        ShowPanel(MainMenuPanel);
    }

    public void OnSaveClicked()
    {
        if (string.IsNullOrEmpty(nameField.text)) return;
        dataManager.SetPlayerName(nameField.text);
        dataManager.SaveResult(count);
        nameField.gameObject.SetActive(false);
        SaveButton.gameObject.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        lastOpenedPanel = "MainMenu";
        BonusManager.Instance.DeactivateBonus();
        text.gameObject.SetActive(false);
        result.text += "Ваш результат: " + text.text;
        AudioManager.instance.PlayLoseSound();
        ShowPanel(DeathPanel);
    }

    public void IncreaseCount(int plus)
    {
        count += plus;
        if (text != null)
            text.text = count.ToString();
    }

    public void OnCitySelected()
    {
        LevelManager.Instance.SetTheme(LevelTheme.City);
        SceneManager.LoadScene("GameScene");
    }

    public void OnRailwaySelected()
    {
        LevelManager.Instance.SetTheme(LevelTheme.Railway);
        SceneManager.LoadScene("GameScene");
    }

    public void SelectPlayer(int index)
    {
        PlayerManager.Instance.SelectPlayer(index);
    }

}