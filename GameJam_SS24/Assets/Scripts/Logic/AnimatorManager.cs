using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;
    void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("horizontal");
        vertical = Animator.StringToHash("vertical");
    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        animator.SetFloat(horizontal, horizontalMovement);
        animator.SetFloat(vertical, verticalMovement);
    }
}
