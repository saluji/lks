using System.Collections;
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

    void Awake()
    {
        if (spawnAmount == 0)
        {
            spawnDelay = 1;
        }
    }

    void Start()
    {
        SpawnKnight();
        SpawnNPC();
    }

    void SpawnKnight()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, knightSpawnPoints.Length);
            Instantiate(knight, knightSpawnPoints[randomIndex].transform.position, knightSpawnPoints[randomIndex].transform.rotation);
            knight.SetActive(true);
            StartCoroutine(SpawnDelay());
        }

    }
    void SpawnNPC()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int randomIndex = Random.Range(0, knightSpawnPoints.Length);
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
