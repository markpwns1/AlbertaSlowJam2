using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Might not keep the attack state depending on progress

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
    private bool isAttacking = false;
    public float rotationSpeed = 5f;

    public override State RunCurrentState()
    {

        Vector3 directionToPlayer = (transform.parent.position - player.position).normalized;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

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

    // Thinking of using this for audio or what ever I have thats available
    public void EnableAttack() => isAttacking = true;
    public void DisableAttack() => isAttacking = false;

    public void OnAttackTriggerDetected(bool isAttackTrigger)
    {
        if (isAttackTrigger)
        {
            Debug.Log("attack trigger interacted");
            EnableAttack();
        }
        else
        {
            DisableAttack();
        }
    }




}
