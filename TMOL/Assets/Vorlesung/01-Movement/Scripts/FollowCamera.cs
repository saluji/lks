using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float smoothTime = 0.25f;

    private Transform Target;
    private Vector3 _velocity;
    private Vector3 offset;

    private void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - Target.position;
    }
    void LateUpdate()
    {
        Vector3 targetPosition = Target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        transform.position = smoothedPosition;
    }
}