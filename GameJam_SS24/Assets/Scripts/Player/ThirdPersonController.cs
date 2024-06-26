using UnityEngine;

/// <summary>
/// Base for a third person character controller
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    // how fast the character can turn
    public float RotationSpeed;

    // Damping for locomotion animator parameter
    public float LocomotionParameterDamping = 0.1f;

    // Animator playing animations
    private Animator _animator;

    // Hash speed parameter
    private int _speedParameterHash;

    // Hash speed parameter
    private int _isWalkingParameterHash;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _speedParameterHash = Animator.StringToHash("speed");
        _isWalkingParameterHash = Animator.StringToHash("isMoving");
    }

    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");

        // Should walk? (left or right shift held)
        //bool shouldWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Set speed to half of input when charakter should walk
        // otherwise use horizontal input
        //float speed = shouldWalk ? verticalInput * 0.333f : verticalInput;

        // Set animator isWalking parameter depending on input
        _animator.SetBool(_isWalkingParameterHash, verticalInput > 0);

        // Set animaotr speed parameter with damping (moves the character via root motion)
        //_animator.SetFloat(_speedParameterHash, speed, LocomotionParameterDamping, Time.deltaTime);
    }
}
