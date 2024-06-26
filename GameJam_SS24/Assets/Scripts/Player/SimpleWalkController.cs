using UnityEngine;
using UnityEngine.UIElements;
public class SimpleWalkController : MonoBehaviour
{
    private Transform CameraTransform;
    public float movementSpeed = 10;
    public float turnSpeed = 90;
    void Start()
    {
        CameraTransform = Camera.main.transform;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection = Quaternion.AngleAxis(CameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        //float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        if (movementDirection != Vector3.zero)
            RotatePlayer(movementDirection);

        MovePlayer(verticalInput);

        /*if (verticalInput > 0)
        {
            RotatePlayer(horizontalInput);
        }*/
    }
    void MovePlayer(float verticalInput)
    {
        transform.Translate(Vector3.back * verticalInput * movementSpeed * Time.deltaTime);
    }
    void RotatePlayer(Vector3 movementDirection)
    {
        Quaternion targetCharacterRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetCharacterRotation, turnSpeed * Time.deltaTime);
    }
    /*void RotatePlayer(float horizontalInput)
    {
        float turnAngle = turnSpeed * horizontalInput * Input.GetAxisRaw("Vertical") * Time.deltaTime;
        transform.Rotate(Vector3.up, turnAngle);
    }*/
}
