using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private RaceTimer raceTimer;
    [SerializeField] private RaceResult raceResult;
    private RaceDataManager raceDataManager;

    private void OnTriggerEnter(Collider other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            raceTimer.StopTimer();
            car.stopInstantly();
            raceResult.Finish(raceTimer.getTime());
        } 
        else
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.stopInstantly();
            }
        }
    }
}
