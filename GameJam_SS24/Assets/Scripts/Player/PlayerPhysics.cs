using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] float raycastDistance = 0.1f;
    [SerializeField] float gravityMultiplier = 1f;
    [SerializeField] float forceStrength = 10f;
    private Animator animator;
    private CharacterController characterController;
    private Vector3 velocity;
    private float ySpeed;
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ySpeed = isGrounded() ? 0 : ySpeed += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Vector3 direction = hit.gameObject.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            rigidbody.AddForceAtPosition(direction * forceStrength * velocity.magnitude, transform.position, ForceMode.Impulse);
        }
    }
    private void OnAnimatorMove()
    {
        animator.ApplyBuiltinRootMotion();
        velocity = animator.deltaPosition;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, raycastDistance * 0.5f, 0), Vector3.down, raycastDistance);
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + new Vector3(0, raycastDistance * 0.5f, 0), Vector3.down * raycastDistance, isGrounded() ? Color.green : Color.red);
    }
}
