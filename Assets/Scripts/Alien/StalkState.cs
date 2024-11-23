using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkState : State
{
    // States
    public TeleportState teleportState;
    public HuntState huntState;
    public AttackState attackState;

    private Rigidbody rb;
    private Renderer alienRender;
    public Transform player;

    public bool inTeleportState = false;
    public bool inHuntState = false;

    // State Ranges
    public float maxRange = 80f;
    public float huntRange = 20f;

    // StalkState Variables
    public float stalkingDistance = 5f;
    public float walkSpeed = 5.5f;
    public float rotationSpeed = 5f;
    public float invisibleDistance = 5.5f;
    public bool isStalking = false;
    //public bool isAttacking = false;

    // Audio Variables
    private AudioSource audioSource;
    public AudioClip footstepClip;
    public float footstepInterval = 0.5f;
    public float footstepAudioInterval = 15f;
    private float footstepTimer;

    // Timer Variables to roll for Hunt State chance
    private float timer = 0f;
    public float checkInterval = 10f;

    public override State RunCurrentState()
    {
        StartStalking();

        // Prevents Model from tipping over
        Vector3 directionToPlayer = (transform.parent.position - player.position).normalized;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        // Finds Distance of player
        float distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);

        //if (isAttacking)
        //{
        //    Debug.Log("going into attack state");
        //    return attackState;
        //}
        
        // Max Range reached goes to Teleport State to keep up with player
        if (distanceToPlayer >= maxRange)
        {

            return teleportState;

        }

        // Wait for Timer to roll chance of Hunt State
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;
            if (distanceToPlayer <= huntRange && ShouldEnterHuntState()) // if plaayer withing range and rolled chance to go into hunt
            {
                StopStalking();
                return huntState;
            }
        }

        // Stalk State
        if (isStalking)
        {
            StalkPlayer();
            PlayFootStepSound();
        }
        

        return this;
    }

    void Start()
    {
        Debug.Log("In Stalk State");
        rb = GetComponentInParent<Rigidbody>();
        alienRender = GetComponentInParent<Renderer>();

        // Audio setup
        audioSource = GetComponentInParent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (footstepClip != null)
        {
            audioSource.clip = footstepClip;
        }
        else
        {
            Debug.LogError("Footstep clip not assigned in StalkState script, its on: " + gameObject.name);
        }
        audioSource.clip = footstepClip;

    }

    private void StalkPlayer()
    {
        // Get Direction to player
        Vector3 directionToPlayer = (player.position - transform.parent.position).normalized;

        // Rotates Alien towards player
        if (directionToPlayer != Vector3.zero)
        {
            // Calculates and estimates desired and current rotation
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.parent.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0)); // rotates
        }

        // Initially was for circling around player, but found this better
        Vector3 offset = transform.parent.position - player.position;
        offset = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0 ) * offset;

        // Calculates and updates alien position
        Vector3 newPosition = player.position + offset;
        rb.MovePosition(newPosition);

        // Moves Alien
        Vector3 forwardMovement = transform.parent.forward * walkSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    private bool ShouldEnterHuntState()
    {
        // Each day after day 1 increases by 25% chance for huntState
        float huntProbability = Mathf.Clamp((SharedData.gameDay - 1) * 25f, 0f, 100f);
        float randomValue = Random.Range(0f, 100f); // chooses random value between 0-100
        Debug.Log("Day: " + SharedData.gameDay + ", Probability: " + huntProbability+"%" + ", Rolled Chance: " + randomValue);
        return randomValue <= huntProbability;
    }

    private void PlayFootStepSound()
    {
        Debug.Log("in audio function");

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f)
        {
            Debug.Log("Playing audio");
            footstepTimer = footstepAudioInterval;
            if (footstepClip != null)
            {
                audioSource.PlayOneShot(footstepClip);
            }
        }
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
        audioSource.Stop();
    }

    public void EnableInvisible() => alienRender.enabled = false;
    public void DisableInvisible() => alienRender.enabled = true;
    //public void EnableAttack() => isAttacking = true;

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


    //public void OnAttackTriggerDetected(bool isAttackTrigger)
    //{
    //    if (isAttackTrigger)
    //    {
    //        Debug.Log("attack trigger interacted");
    //        EnableAttack();
    //    } 
    //    //else
    //    //{
    //    //    isAttacking= false;
    //    //}
    //}
}
