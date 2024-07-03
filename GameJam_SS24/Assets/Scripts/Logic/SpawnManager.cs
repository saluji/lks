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

    void Start()
    {
        SpawnKnight();
        SpawnNPC();
    }

    void SpawnKnight()
    {
        for (int i = 0; i < knightSpawnPoints.Length; i++)
        {
            Instantiate(knight, knightSpawnPoints[i].transform.position, knightSpawnPoints[i].transform.rotation);
            knight.SetActive(true);
            StartCoroutine(SpawnDelay());
        }

    }
    void SpawnNPC()
    {
        for (int i = 0; i < npcSpawnPoints.Length; i++)
        {
            Instantiate(npc, npcSpawnPoints[i].transform.position, npcSpawnPoints[i].transform.rotation);
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
