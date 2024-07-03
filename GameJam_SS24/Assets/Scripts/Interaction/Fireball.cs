using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    void Start()
    {
        // Startet den Timer, um den Fireball nach 5 Sekunden zu zerstören
        StartCoroutine(DestroyAfterTime(5f));
    }

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
        else if (collider.gameObject.CompareTag("Destructable"))
        {
            collider.gameObject.AddComponent<Rigidbody>();
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
