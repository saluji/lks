using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject knights;
    // [SerializeField] GameObject npc;
    [SerializeField] Transform spawnPoint;

    void Start()
    {
        SpawnNPC();
    }

    void SpawnNPC()
    {
        Instantiate(knights, spawnPoint.transform.position, spawnPoint.transform.rotation);
        knights.SetActive(true);
        // Instantiate(npc, spawnPoint, spawnPoint);
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);
        SpawnNPC();
    }
}
