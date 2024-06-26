using UnityEngine;
public class ThirdPersonController : MonoBehaviour
{
    // how fast the character can turn
    public float rotationSpeed;

    // Damping for locomotion animator parameter
    public float LocomotionParameterDamping = 0.1f;

    // Animator playing animations
    private Animator animator;

    // Hash speed parameter
    private int speedParameterHash;

    // Hash speed parameter
    private int isWalkingParameterHash;

    // Main camera
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speedParameterHash = Animator.StringToHash("speed");
        isWalkingParameterHash = Animator.StringToHash("isWalking");
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
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
        //animator.SetFloat(speedParameterHash, speed, LocomotionParameterDamping, Time.deltaTime);

        if (movementDirection != Vector3.zero)
            RotatePlayer(movementDirection);
    }
    void RotatePlayer(Vector3 movementDirection)
    {
        Quaternion targetCharacterRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCharacterRotation, rotationSpeed * Time.deltaTime);

    }
}
