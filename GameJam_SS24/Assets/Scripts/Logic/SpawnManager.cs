using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject knights;
<<<<<<< Updated upstream
    [SerializeField] float seconds;
    // [SerializeField] GameObject npc;
    [SerializeField] Transform spawnPoint;
=======
    [SerializeField] GameObject npc;
    [SerializeField] Transform knightSpawnPoint;
    [SerializeField] Transform npcSpawnPoint;
    [SerializeField] float delay;
>>>>>>> Stashed changes

    void Start()
    {
        SpawnNPC();
    }

    void SpawnNPC()
    {
        Instantiate(knights, knightSpawnPoint.transform.position, knightSpawnPoint.transform.rotation);
        knights.SetActive(true);
        Instantiate(npc, npcSpawnPoint.transform.position, npcSpawnPoint.transform.rotation);
        npc.SetActive(true);
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
<<<<<<< Updated upstream
        yield return new WaitForSeconds(seconds);
=======
        yield return new WaitForSeconds(delay);
>>>>>>> Stashed changes
        SpawnNPC();
    }
}
