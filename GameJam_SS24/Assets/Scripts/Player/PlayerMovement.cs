using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float turnSmoothTime = 0.1f;
    private Transform cam;
    private float turnSmoothVelocity;
    void Start()
    {
        cam = Camera.main.transform;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (direction.magnitude >= 0.1f)
            MovePlayer(direction);
    }
    void MovePlayer(Vector3 direction)
    {
        // smooth and rotate player
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // move player relative to camera
        Vector3 movementDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(movementDirection.normalized * movementSpeed * Time.deltaTime);
    }
}
