using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mushroomPrefabs;
    [SerializeField] private GameObject _spawnPoints;
    public float spawnInterval = 2f;

    private void Start()
    {
        SpawnMushrooms();
    }

    void SpawnMushrooms()
    {
        for (int i = 0; i < 30; i++)
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
                int randomSpawnPointIndex = Random.Range(0, _spawnPoints.transform.childCount);
                Transform spawnPoint = _spawnPoints.transform.GetChild(randomSpawnPointIndex);
                Instantiate(mushroom, spawnPoint.position, Quaternion.identity);
                break;
            }
        }
    }
}
