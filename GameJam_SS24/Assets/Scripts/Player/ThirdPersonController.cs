using UnityEngine;
public class ThirdPersonController : MonoBehaviour
{
    public float rotationSpeed;
    public float locomotionParameterDamping = 0.1f;
    private Animator animator;
    private int speedParameterHash;
    private int isWalkingParameterHash;
    private Transform cameraTransform;
    void Start()
    {
        animator = GetComponent<Animator>();
        speedParameterHash = Animator.StringToHash("speed");
        isWalkingParameterHash = Animator.StringToHash("isWalking");
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Rotate input vector depending on camera y-Rotation
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        // Should walk? (left or right shift held)
        bool shouldWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Set speed to half of input when charakter should walk
        // otherwise use horizontal input
        float speed = shouldWalk ? inputMagnitude * 2 : inputMagnitude;

        // Set animator isWalking parameter depending on input
        animator.SetBool(isWalkingParameterHash, inputMagnitude > 0);

        // Set animaotr speed parameter with damping (moves the character via root motion)
        animator.SetFloat(speedParameterHash, speed, locomotionParameterDamping, Time.deltaTime);

        if (movementDirection != Vector3.zero)
            RotatePlayer(movementDirection);
    }
    void RotatePlayer(Vector3 movementDirection)
    {
        Quaternion targetCharacterRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCharacterRotation, rotationSpeed * Time.deltaTime);

    }
}
