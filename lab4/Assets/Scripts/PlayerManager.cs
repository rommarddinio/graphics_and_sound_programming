using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Player Prefabs")]
    public GameObject[] playerPrefabs;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    private Character currentPlayer;

    public Character CurrentPlayer => currentPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (!HasPlayer())
        {
            if (playerPrefabs.Length > 1)
            {
                GameObject obj = playerPrefabs[0];
                currentPlayer = obj.GetComponent<Character>();
                playerPrefabs[1].SetActive(false);
                if (currentPlayer == null)
                {
                    Debug.LogError("На префабе нет компонента Character!");
                }
            }
            else
            {
                Debug.LogError("Нет второго префаба игрока в массиве!");
            }
        }
    }

    public void SelectPlayer(int index)
    {
        if (index < 0 || index >= playerPrefabs.Length)
        {
            Debug.LogError("Неверный индекс игрока!");
            return;
        }

        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            if (playerPrefabs[i] != null)
            {
                playerPrefabs[i].SetActive(i == index); 
            }
        }

        GameObject selectedObj = playerPrefabs[index];
        currentPlayer = selectedObj.GetComponent<Character>();

        if (currentPlayer == null)
        {
            Debug.LogError("На префабе нет компонента Character!");
        }
    }

    public Transform GetPlayerTransform()
    {
        if (currentPlayer == null) return null;
        return currentPlayer.transform;
    }

    public bool HasPlayer()
    {
        return currentPlayer != null;
    }
}