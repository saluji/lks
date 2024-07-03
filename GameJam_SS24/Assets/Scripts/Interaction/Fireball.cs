using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Rendering;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;
    [SerializeField] float explosionForce = 5f;
    [SerializeField] float explosionRadius = 5f;

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
        //     if (collider.gameObject.CompareTag("Destructable"))
        //     {
        //         BreakObject();
        //     }
        // }
        // void BreakObject()
        // {
        //     gameObject.SetActive(false);

        //     foreach (GameObject piece in pieces)
        //     {
        //         GameObject instantiatedPiece = Instantiate(piece, transform.position, transform.rotation);
        //         instantiatedPiece.AddComponent<Rigidbody>();
        //         Rigidbody rb = instantiatedPiece.GetComponent<Rigidbody>();
        //         if (rb != null)
        //         {
        //             rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        //         }
        //     }
    }
}
