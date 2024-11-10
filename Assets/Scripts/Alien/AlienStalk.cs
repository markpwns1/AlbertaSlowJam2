using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStalk : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer alienRender;
    public Transform player;
    public float stalkingDistance = 5f;
    public float walkSpeed = 5.5f;
    public float rotationSpeed = 5;
    public float invisibleDistance = 5.5f;
    public bool isStalking = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        alienRender = GetComponent<Renderer>();

        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }
    void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);

        if (isStalking)
        {
           

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            //float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            float angle = rotationSpeed * Time.deltaTime;
            transform.RotateAround(player.position, Vector3.up, angle);

            Vector3 movement = transform.forward * walkSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);


        }
    }

    public void StartStalking()
    {
        isStalking = true;
    }
    public void StopStalking()
    {
        isStalking= false;
    }
    public void EnableInvisible()
    {
        alienRender.enabled = false;
    }
    public void DisableInvisible()
    {
        alienRender.enabled = true;
    }

    public void OnTriggerDetected(bool isPlayerInsideTrigger)
    {
        if (isPlayerInsideTrigger)
        {
            StartStalking();
            EnableInvisible();
        } else if (!isPlayerInsideTrigger)
        {
            StopStalking();
            DisableInvisible();
        }
    }
  
}
