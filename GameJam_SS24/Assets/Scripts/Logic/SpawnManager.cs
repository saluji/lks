using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject knight;
    [SerializeField] Transform[] knightSpawnPoints;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] int maxKnights = 10; // Set a limit

    private int knightCount = 0;

    void Start()
    {
        StartCoroutine(SpawnKnight());
    }

    IEnumerator SpawnKnight()
    {
        while (knightCount < maxKnights)
        {
            int randomIndex = Random.Range(0, knightSpawnPoints.Length);
            Instantiate(knight, knightSpawnPoints[randomIndex].position, knightSpawnPoints[randomIndex].rotation);
            knightCount++; // Track the number of spawned knights
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
