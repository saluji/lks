using UnityEngine;

public class AmyAnimationController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float turnSpeed = 90f;
    private Animator animator;
    private int isWalkingParameterHash;
    private int isGreetingParameterHash;
    private int isDancingParameterHash;
    private int directionParameterHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingParameterHash = Animator.StringToHash("isWalking");
        isGreetingParameterHash = Animator.StringToHash("isGreeting");
        isDancingParameterHash = Animator.StringToHash("isDancing");
        directionParameterHash = Animator.StringToHash("direction");
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float verticalInputRaw = Input.GetAxisRaw("Vertical");

        //Walk forward
        transform.Translate(Vector3.forward * verticalInput * movementSpeed * Time.deltaTime);
        //Walk animation
        animator.SetBool(isWalkingParameterHash, verticalInputRaw != 0);
        animator.SetFloat(directionParameterHash, verticalInputRaw);
        if (verticalInputRaw != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat(directionParameterHash, verticalInputRaw * 2);
        }

        //Walking animation & rotation
        if (verticalInput > 0)
        {
            float turnAngle = turnSpeed * horizontalInput * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAngle);
        }
        //Greeting animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger(isGreetingParameterHash);
        }
        //Dance animatioin
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bool isDancing = animator.GetBool(isDancingParameterHash);
            animator.SetBool(isDancingParameterHash, !isDancing);
        }
    }
}
