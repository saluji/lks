using UnityEngine;

/// <summary>
/// Base class for all senses
/// </summary>
[ExecuteInEditMode]
public abstract class Sense : MonoBehaviour
{
    // Range of the sense (meter)
    public float range;

    // Head in Mixamo-Rig
    public Transform headReferenceTransform;

    // Wird der Spieler gerade wahrgenommen
    public bool isDetecting { get; protected set; }

    // Direction vector to the player
    protected Vector3 directionToPlayer;

    // Transform of player
    public Transform player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    protected virtual void Update()
    {
        directionToPlayer = player.transform.position - headReferenceTransform.position;
    }

    // Player in range?
    public bool IsInRange()
    {
        return directionToPlayer.sqrMagnitude <= (range * range);
    }

}