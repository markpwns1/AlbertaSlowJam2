using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    public Rigidbody rb;
    public StalkState stalkState;
    public bool inStalkState = false;
    public float minRange = 60f;
    public float rotationSpeed = 5f;

    private Renderer alienRender;
    public Transform player;
    public float teleportOffset = 40f;
    public bool isPlayerTooFar = false;

    public override State RunCurrentState()
    {
        Debug.Log("In Teleport State");
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        if (distanceToPlayer <= minRange )
        {
            return stalkState;
        }

        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);

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
        Debug.Log("Alien Creeps closer");
        Vector3 directionToPlayer = (transform.parent.position - player.position).normalized;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        Vector3 newPosition = player.position + directionToPlayer * teleportOffset;

        rb.position = newPosition;
        //Vector3 newPosition = player.position + directionToPlayer * teleportOffset;
        //transform.parent.position = newPosition;
    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;

    public void OnTriggerDetected(bool isPlayerOutsideRange)
    {
        isPlayerTooFar = isPlayerOutsideRange;
    }
}
