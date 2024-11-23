using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    public Rigidbody rb;
    private Renderer alienRender;
    public Transform player;

    // States
    public StalkState stalkState;

    // Teleport State Variables
    public bool inStalkState = false;
    public float minRange = 60f;
    public float maxRange = 80f;
    public float rotationSpeed = 5f;
    public float teleportOffset = 40f;
    public bool isPlayerTooFar = false;

    public override State RunCurrentState()
    {
        EnableInvisible();
        Debug.Log("In Teleport State");
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        // Prevents Model from tipping over
        Vector3 directionToPlayer = (transform.parent.position - player.position).normalized;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }
        // If player within range, switch to Stalk State
        if (distanceToPlayer <= minRange )
        {
            return stalkState;
        }

        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);
         
        // enables booleon to trigger teleport state
        if (distanceToPlayer >= maxRange )
        {
            isPlayerTooFar=true;
        }
        // Teleport State
        if (isPlayerTooFar )
        {
            TeleportToPlayer();
            isPlayerTooFar = false;
        }

        return this;
    }

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        alienRender = GetComponentInParent<Renderer>();
        EnableInvisible();
        
    }

    private void TeleportToPlayer()
    {
        EnableInvisible();
        Debug.Log("Alien Creeps closer");

        // Random Teleport Positions to bring into Stalk State between 40-50 units
        float randomDistance = Random.Range(40f, 50f);

        // Teleports Alien to random direction of player 
        Vector3 randomDirection = Random.insideUnitSphere.normalized * randomDistance;
        randomDirection.y = 0; // Ensures movement is horizontal

        // Final Position for teleport
        Vector3 intendedPosition = player.position + randomDirection;

        // Checks for ground to prevent clipping through map due to mountainous terrain
        Vector3 raycastOrigin = new Vector3(intendedPosition.x, intendedPosition.y + 300f, intendedPosition.z);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, 300f))
        {
            // If ground surface found , set intended position to hit point
            intendedPosition = hit.point;
            Debug.Log($"Teleported to ground point at {intendedPosition}");
        }
        else
        {
            Debug.Log("Failed to teleport to ground. Aborting teleport.");
            return; // Abort teleport if no ground is found
        }

        // Teleports the alien
        rb.position = intendedPosition;

        // Align the alien's rotation to keep it up right
        transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.parent.rotation;

    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;

    public void OnTriggerDetected(bool isPlayerOutsideRange)
    {
        isPlayerTooFar = isPlayerOutsideRange;
    }
}
