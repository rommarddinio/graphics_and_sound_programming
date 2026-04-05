using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public UIManager manager;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        manager.IncreaseCount();
    }

}
