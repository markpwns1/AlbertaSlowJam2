using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public HuntState huntState;

    private Rigidbody rb;
    public Transform player;

    public float attackRange = 5f;
    public float jumpForce = 15f;
    public float jumpDuration = 1f;
    private bool hasJumped = false;
    private bool resetPosition = false;
    private float jumpTimer;

    public override State RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        if (distanceToPlayer <= attackRange)
        {
            JumpToPlayer();
        }

        if (distanceToPlayer > attackRange)
        {
            
            return huntState;
        }

        return this;
    }

    void Start()
    {
        Debug.Log("In Attack State");
        rb = GetComponentInParent<Rigidbody>();
        jumpTimer = jumpDuration;
        hasJumped = false;
        resetPosition = false;

    }

    private void JumpToPlayer()
    {
        if (!hasJumped)
        {
            Vector3 directionToPlayer = (player.position - transform.parent.position).normalized;

            Vector3 jumpVector = directionToPlayer * jumpForce + Vector3.up * (jumpForce / 2);
            rb.AddForce(jumpVector, ForceMode.Impulse);

            hasJumped = true;
        }
        
    }




}
