using UnityEngine;

public class BreakOnMouseClick : MonoBehaviour
{
    public GameObject[] pieces; // Array der Einzelteile
    public float explosionForce = 5f; // Kraft der Explosion
    public float explosionRadius = 5f; // Radius der Explosion

    private bool isBroken = false; // Um sicherzustellen, dass das Objekt nur einmal zerfällt

    void Update()
    {
        
    }

    void BreakObject()
    {
        isBroken = true;

        // Deaktiviere das ursprüngliche Objekt
        gameObject.SetActive(false);

        // Erstelle die Einzelteile
        foreach (GameObject piece in pieces)
        {
            GameObject instantiatedPiece = Instantiate(piece, transform.position, transform.rotation);
            Rigidbody rb = instantiatedPiece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Füge eine Explosion hinzu, um die Teile auseinander zu bewegen
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    void OnMouseDown()
    {
        if (!isBroken)
        {
            BreakObject();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            BreakObject();
        }
    }
}
