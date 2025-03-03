/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject knight;
    [SerializeField] GameObject npc;
    [SerializeField] Transform[] knightSpawnPoints;
    [SerializeField] Transform[] npcSpawnPoints;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnAmount = 1;

    void Start()
    {
        SpawnKnight();
        SpawnNPC();
    }

    void SpawnKnight()
    {
        for (int i = 0; i <= spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, knightSpawnPoints.Length);
            Instantiate(knight, knightSpawnPoints[randomIndex].transform.position, knightSpawnPoints[randomIndex].transform.rotation);
            knight.SetActive(true);
            StartCoroutine(SpawnDelay());
        }
    }
    void SpawnNPC()
    {
        for (int i = 0; i <= spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, npcSpawnPoints.Length);
            Instantiate(npc, npcSpawnPoints[randomIndex].transform.position, npcSpawnPoints[randomIndex].transform.rotation);
            npc.SetActive(true);
            StartCoroutine(SpawnDelay());
        }

    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnKnight();
        SpawnNPC();
    }
}
*/

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
