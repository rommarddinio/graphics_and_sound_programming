using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    private bool ready = false;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CarController car;
    [SerializeField] private EnemyController[] enemies;
    [SerializeField] private RaceTimer raceTimer;
    
    public void setReady(bool ready)
    {
        this.ready = ready;
    }

    public void Start()
    {
        countdownText.SetText(string.Empty);
    }
    private void Update()
    {
        if (ready) 
        { 
            StartCoroutine(CountdownRoutine());
            ready = false;
        }
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";

        car.setMove(true);
        foreach (var car in enemies)
            car.setMove(true);
        raceTimer.StartTimer();

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }
}

