using UnityEngine;

/// <summary>
/// Controll a blend tree
/// </summary>
public class BlendTreeController : MonoBehaviour
{
    //[SerializeField] float movementSpeed = 2;
    [SerializeField] float locomotionParameterDamping = 0.1f;
    private Animator animator;
    private int speedParameterHash;
    private int isWalkingParameterHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        speedParameterHash = Animator.StringToHash("speed");
        isWalkingParameterHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");
        bool shouldWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float speed = shouldWalk ? verticalInput * 2: verticalInput;
        //animator.SetFloat(speedParameterHash, speed);
        animator.SetFloat(speedParameterHash, speed, locomotionParameterDamping, Time.deltaTime);
        if (verticalInput == 0)
        {
            animator.SetBool(isWalkingParameterHash, false);
        }
        else
        {
            animator.SetBool(isWalkingParameterHash, true);
        }
        //Move character
        //transform.Translate(Vector3.forward * movementSpeed * verticalInput * Time.deltaTime);
    }
}
