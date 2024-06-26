using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody rb;
    [SerializeField] float rotationSpeed;
    void Update()
    {
        // rotate orientation
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        // rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (inputDirection != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
