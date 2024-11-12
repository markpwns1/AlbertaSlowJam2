using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkState : State
{
    public TeleportState teleportState;
    public HuntState huntState;
    public bool inTeleportState = false;
    public bool inHuntState = false;
    public float maxRange = 80f;
    public float huntRange = 20f;

    private Rigidbody rb;
    private Renderer alienRender;
    public Transform player;
    public float stalkingDistance = 5f;
    public float walkSpeed = 5.5f;
    public float rotationSpeed = 5f;
    public float invisibleDistance = 5.5f;
    public bool isStalking = false;

    public override State RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        if (distanceToPlayer >= maxRange)
        {
            return teleportState;

        } else if (distanceToPlayer <= huntRange)
        {
            return huntState;
        }

        if (isStalking)
        {
            StalkPlayer();
        }

        return this;
    }

    void Start()
    {
        Debug.Log("In Stalk State");
        rb = GetComponentInParent<Rigidbody>();
        alienRender = GetComponentInParent<Renderer>();
        //rb.centerOfMass = new Vector3(0, -0.5f, 0);

    }

    private void StalkPlayer()
    {
        // Get Direction to player
        Vector3 directionToPlayer = (player.position - transform.parent.position).normalized;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        Vector3 offset = transform.parent.position - player.position;
        offset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0 ) * offset;

        Vector3 newPosition = player.position + offset;
        rb.MovePosition(newPosition);

        
        Vector3 forwardMovement = transform.parent.forward * walkSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
        //Vector3 movement = transform.parent.forward * walkSpeed * Time.deltaTime;
        //rb.MovePosition(transform.parent.position + movement);

    }

    public void StartStalking()
    {
        isStalking=true;
        EnableInvisible();
    }

    public void StopStalking()
    {
        isStalking = false;
        DisableInvisible();
    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;

    public void OnTriggerDetected(bool isPlayerInsideTrigger)
    {
        if (isPlayerInsideTrigger)
        {
            StartStalking();
        } else
        {
            StopStalking();
        }
    }
}
