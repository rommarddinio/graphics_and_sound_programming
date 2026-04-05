using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public Transform player;

    public int initialPlatforms = 5;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float spawnZ = 0f;
    public float spawnOffset = 0f;

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player.position.z + 2 * GetLastPlatformLength() > spawnZ)
        {
            SpawnPlatform();
            DeleteOldPlatform();
        }
    }

    void SpawnPlatform()
    {
        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
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