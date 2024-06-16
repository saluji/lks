using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform destination;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ball"))
        {
            CharacterController ccScript = other.gameObject.GetComponent<CharacterController>();
            ccScript.enabled = false;
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
            ccScript.enabled = true;
        }
    }
}
