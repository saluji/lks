using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float smoothTime = 0.25f;
    private Transform target;
    private Vector3 velocity;
    private Vector3 offset;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}