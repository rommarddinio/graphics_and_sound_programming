using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public UIManager manager;
    private bool bonus = false;

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
        if (!bonus) { 
            manager.IncreaseCount(1);
        }
        else {
            manager.IncreaseCount(2);
        }
        AudioManager.instance.PlayCoinSFX();
    }

    public void SetBonus(bool bonus)
    {
        this.bonus = bonus;
    }

}
