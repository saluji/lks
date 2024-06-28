
/// <summary>
/// Implementation of virtual ears
/// </summary>
public class Ears : Sense
{
    private PlayerStateMachine _playerController;

    protected override void Start()
    {
        base.Start();

        _playerController = _player.GetComponent<PlayerStateMachine>();
    }

    protected override void Update()
    {
        base.Update();

        if(IsInRange() && _playerController.IsAudible) 
        {
            IsDetecting = true;
        }
        else
        {
            IsDetecting = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        SenseGizmos.DrawRangeDisc(HeadReferenceTransform.position, transform.up, Range);
    }
#endif
}
