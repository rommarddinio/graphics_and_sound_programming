using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bonus : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        BonusManager.Instance.ActivateBonus();
        Destroy(gameObject);
        AudioManager.instance.PlayBonusSFX();
    }
}