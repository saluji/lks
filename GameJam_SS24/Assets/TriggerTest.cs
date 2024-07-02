using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        other.gameObject.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        other.gameObject.SetActive(true);
    }
}
