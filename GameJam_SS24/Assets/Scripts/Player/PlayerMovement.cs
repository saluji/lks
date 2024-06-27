using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float turnSmoothTime = 0.1f;
    private AnimatorManager animatorManager;
    private Transform cam;
    private float moveAmount;
    private float turnSmoothVelocity;
    void Start()
    {
        animatorManager = GetComponent<AnimatorManager>();
        cam = Camera.main.transform;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (direction.magnitude >= 0.1f)
        {
            MovePlayer(direction, horizontalInput, verticalInput);
        }
    }
    void MovePlayer(Vector3 direction, float horizontalInput, float verticalInput)
    {
        // smooth and rotate player
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // move player relative to camera
        Vector3 movementDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(movementDirection.normalized * movementSpeed * Time.deltaTime);

        // animate player
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}