using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    private Rigidbody rb;
    private Renderer alienRender;
    public Transform player;

    // Hunt State Variables
    public float walkSpeed;
    public float rotationSpeed = 5f;
    public float maxRange = 80f;
    public bool isAttacking = false;
    public float attackRange = 5f;

    // States
    public TeleportState teleportState;
    public AttackState attackState;

    public bool inTeleportState;


    public override State RunCurrentState()
    {
        // Prevents Model from tipping over
        Vector3 directionToPlayer = (transform.parent.position - player.position).normalized;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        Debug.Log("in Hunt State");
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        // if player goes over Max Range, alien resets to teleport state (player escaped)
        if (distanceToPlayer >= maxRange )
        {
            EnableInvisible();
            inTeleportState = true;
            return teleportState;
        }
        //if (distanceToPlayer <= attackRange)
        //{
        //    Debug.Log("going into attack State");
        //    return attackState;
        //}
        
        // Hunt State
        MoveTowardsPlayer();
        return this;
    }

    void Start()
    {
        Debug.Log("In Hunt State");
        rb = GetComponentInParent<Rigidbody>();
        alienRender = GetComponentInParent<Renderer>();
        DisableInvisible();

        // Change Alien speed variable based on day/difficulty
        WalkSpeedByDay();
        
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

    private void WalkSpeedByDay()
    {
        float baseSpeed = 5f; // initial speed
        float speedIncrement = 2f; // speed goes up by 2 each day

        // 5+ Days being the most difficult
        int effectiveDay = Mathf.Clamp(SharedData.gameDay, 1, 5);

        // Adjust Alien Speed based on Day
        walkSpeed = baseSpeed + (effectiveDay -1) * speedIncrement;

        Debug.Log("Day: "+SharedData.gameDay+", Effective Day: "+effectiveDay+", Walk Speed: "+walkSpeed);
    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;
    //public void EnableAttack() => isAttacking = true;
    //public void DisableAttack() => isAttacking = false;

    //public void OnAttackTriggerDetected(bool isAttackTrigger)
    //{
    //    if (isAttackTrigger)
    //    {
    //        Debug.Log("attack trigger interacted");
    //        EnableAttack();
    //    }
    //    else
    //    {
    //        DisableAttack();
    //    }
    //}

}
