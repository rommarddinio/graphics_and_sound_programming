using System.Collections.Generic;
using UnityEngine;

public enum LevelTheme
{
    City,
    Railway
}

public class PlatformSpawner : MonoBehaviour
{
    [Header("Платформы")]
    public GameObject[] cityPlatforms;
    public GameObject[] railwayPlatforms;

    private GameObject[] currentPlatforms;

    public Transform player;

    public int initialPlatforms = 5;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float spawnZ = 0f;
    public float spawnOffset = 0f;

    void Start()
    {
        if (LevelManager.Instance != null)
        {
            if (LevelManager.Instance.selectedTheme == LevelTheme.City)
                currentPlatforms = cityPlatforms;
            else
                currentPlatforms = railwayPlatforms;
        }
        else
        {
            currentPlatforms = cityPlatforms;
        }

        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        Transform currentPlayer = PlayerManager.Instance.GetPlayerTransform();
        if (currentPlayer == null) return;

        if (currentPlayer.position.z + 2 * GetLastPlatformLength() > spawnZ)
        {
            SpawnPlatform();
            DeleteOldPlatform();
        }
    }

    void SpawnPlatform()
    {
        GameObject prefab = currentPlatforms[Random.Range(0, currentPlatforms.Length)];
        GameObject go = Instantiate(prefab);

        float length = GetPlatformLength(go);

        if (activePlatforms.Count == 0)
        {
            go.transform.position = new Vector3(0, 0, 0);
            spawnZ = length / 2;
        }
        else
        {
            go.transform.position = new Vector3(0, 0, spawnZ + spawnOffset + length / 2);
            spawnZ += length + spawnOffset;
        }

        activePlatforms.Add(go);
    }

    void DeleteOldPlatform()
    {
        if (activePlatforms.Count > initialPlatforms)
        {
            Destroy(activePlatforms[0]);
            activePlatforms.RemoveAt(0);
        }
    }

    float GetLastPlatformLength()
    {
        if (activePlatforms.Count == 0) return 0f;
        return GetPlatformLength(activePlatforms[activePlatforms.Count - 1]);
    }

    float GetPlatformLength(GameObject platform)
    {
        Renderer[] renderers = platform.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 10f;

        float minZ = float.MaxValue;
        float maxZ = float.MinValue;

        foreach (Renderer r in renderers)
        {
            minZ = Mathf.Min(minZ, r.bounds.min.z);
            maxZ = Mathf.Max(maxZ, r.bounds.max.z);
        }

        return maxZ - minZ;
    }
}