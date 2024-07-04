using UnityEngine;

public class Fireball : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] float speed = 10;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            Destroy(collider.gameObject);
            // audioManager.PlaySFX(audioManager.explosion);
        }
    }
}
