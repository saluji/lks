using UnityEngine;

/// <summary>
/// Control the locomotion of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    // how fast the character can turn
    public float rotationSpeed;

    // Damping for locomotion animator parameter
    public float locomotionParameterDamping = 0.1f;

    // Animator playing animations
    private Animator animator;

    // Hash speed parameter
    private int speedParameterHash;

    // Hash speed parameter
    private int isWalkingParameterHash;

    // Cache the camera transform
    private Transform cameraTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        speedParameterHash = Animator.StringToHash("speed");
        isWalkingParameterHash = Animator.StringToHash("isMoving");

        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Create Vector from inputs
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Should walk? (left or right shift held)
        bool shouldWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Set speed to half of input when charakter should walk
        // otherwise use horizontal input
        float speed = shouldWalk ? inputMagnitude * 0.333f : inputMagnitude;

        // Set animator isWalking parameter depending on input
        animator.SetBool(isWalkingParameterHash, inputMagnitude > 0);

        // Set animaotr speed parameter with damping (moves the character via root motion)
        animator.SetFloat(speedParameterHash, speed, locomotionParameterDamping, Time.deltaTime);

        // Make movement direction depend on camera rotation
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) 
            * movementDirection;

        // Rotate the character to movement direction
        if(movementDirection != Vector3.zero) 
        {
            Quaternion targetCharacterRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCharacterRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
