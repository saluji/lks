using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 10;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("NPC"))
        {
            Destroy(gameObject);
        }
    }
}
