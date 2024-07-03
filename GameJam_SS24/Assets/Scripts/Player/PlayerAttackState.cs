using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.AppliedMovementX = Ctx.AppliedMovementZ = Ctx.TurnSpeed = 0;
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
        Ctx.AnimationLength = Time.time + 0.5f;
        Object.Instantiate(Ctx.Fireball, Ctx.JawPosition.position, Ctx.JawPosition.rotation);
        Ctx.IsJumpable = false;
    }

    public override void UpdateState()
    {
        // Ctx.Fireball.transform.position += Ctx.Fireball.transform.forward;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Attack: Exit");
        Ctx.TurnSpeed = 15;
        // Object.Destroy(Ctx.Fireball);
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
        Ctx.IsJumpable = true;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        Ctx.Fireball.transform.Translate(Vector3.forward * Time.deltaTime);
        if (Time.time > Ctx.AnimationLength)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}
