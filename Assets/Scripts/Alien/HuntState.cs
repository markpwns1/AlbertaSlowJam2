using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    private Rigidbody rb;
    private Renderer alienRender;
    public Transform player;
    public float walkSpeed = 9f;
    public float rotationSpeed = 5f;
    public float maxRange = 80f;

    public TeleportState teleportState;
    public bool inTeleportState;


    public override State RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);
        if (distanceToPlayer >= maxRange )
        {
            EnableInvisible();
            inTeleportState = true;
            return teleportState;
        }

        MoveTowardsPlayer();
        return this;
    }

    void Start()
    {
        Debug.Log("In Hunt State");
        rb = GetComponentInParent<Rigidbody>();
        alienRender = GetComponentInParent<Renderer>();
        DisableInvisible();
    }

    private void MoveTowardsPlayer()
    {
        // Get Direction to player
        Vector3 directionToPlayer = (player.position - transform.parent.position).normalized;

        // Rotates towards player and prevents alien from tipping over
        if (directionToPlayer !=  Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        // Moves alien towards player
        Vector3 movement = transform.parent.forward * walkSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.parent.position + movement);
    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;


}
