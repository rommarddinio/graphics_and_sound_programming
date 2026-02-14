using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public TMP_Text timerText;   
    [SerializeField] private CarController playerCar;
    private float elapsedTime = 0f;
    private bool isTiming = false;

    public void Start()
    {
        timerText.SetText(string.Empty);
    }

    public float getTime()
    {
        return elapsedTime;
    }
    public void StartTimer()
    {
        elapsedTime = 0f;
        isTiming = true;
        playerCar.setMove(true);
    }

    public void StopTimer()
    {
        isTiming = false;
        playerCar.setMove(false);
    }

    private void Update()
    {
        if (!isTiming) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
