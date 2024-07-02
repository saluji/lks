
/// <summary>
/// Implementation of virtual ears
/// </summary>
public class Ears : Sense
{
    private PlayerStateMachine playerController;

    protected override void Start()
    {
        base.Start();

        playerController = player.GetComponent<PlayerStateMachine>();
    }

    protected override void Update()
    {
        base.Update();
        isDetecting = (IsInRange() && playerController.IsAudible) ? true : false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        SenseGizmos.DrawRangeDisc(headReferenceTransform.position, transform.up, range);
    }
#endif
}