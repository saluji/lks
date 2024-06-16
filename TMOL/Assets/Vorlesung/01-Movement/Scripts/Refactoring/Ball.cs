using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

    // Bewegungs-Geschwindigkeit
    public float speed = 0;

    // Sprung Stärke
    public float jumpStrength = 50;

    // Rigidbody of the player.
    private Rigidbody _rb;

    // Bewegung entlang der X- und Y-Achse.
    private float _movementX;
    private float _movementY;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementX = Input.GetAxis("Horizontal");
        _movementY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jumping();
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 Movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(Movement * speed);
    }

    private void Jumping()
    {
        _rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.6f))
        {
            print(hit.collider.name);
            return true;
        }
        else
        {
            return false;
        }
    }
}
