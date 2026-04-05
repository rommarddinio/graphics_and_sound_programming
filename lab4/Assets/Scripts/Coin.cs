using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            CoinManager.Instance.AddCoin(); 
            Destroy(gameObject);            
        }
    }
}