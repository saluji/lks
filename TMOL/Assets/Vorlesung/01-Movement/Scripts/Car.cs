using System.Runtime.CompilerServices;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float turnSpeed = 5;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float verticalRaw = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * vertical);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * verticalRaw * horizontal);
    }
}
