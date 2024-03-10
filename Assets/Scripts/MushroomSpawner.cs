using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mushroomPrefabs;
    [SerializeField] private GameObject _spawnPoints;

    private List<Transform> spawnPoints;
    public float spawnInterval = 2f;

    private void Start()
    {
        spawnPoints = new List<Transform>();

        foreach (Transform child in _spawnPoints.transform)
        {
            spawnPoints.Add(child);
        }
        SpawnMushrooms();
    }

    void SpawnMushrooms()
    {
        for (int i = 0; i < 50; i++)
        {
            SpawnMushroom();
        }
    }
    
    void SpawnMushroom()
    {
        float totalProbability = 0f;
        foreach (GameObject mushroom in _mushroomPrefabs)
        {
            totalProbability += mushroom.GetComponentInChildren<Mushroom>().spawnProbability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (GameObject mushroom in _mushroomPrefabs)
        {
            cumulativeProbability += mushroom.GetComponentInChildren<Mushroom>().spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
                Transform spawnPoint = spawnPoints[randomSpawnPointIndex];
                GameObject instantiatedObject = Instantiate(mushroom, spawnPoint.position, Quaternion.identity);
                instantiatedObject.transform.SetParent(gameObject.transform, true);
                Destroy(spawnPoint.gameObject);
                spawnPoints.RemoveAt(randomSpawnPointIndex);
                break;
            }
        }
    }
}
