using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWalk : MonoBehaviour
{
    private Rigidbody rb;
    public Transform player;
    public float walkSpeed = 9f;
    public float rotationSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("player position: " + player.position);
        // Get Direction to player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Rotates towards player and prevents alien from tipping over
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion alienRotation = Quaternion.LookRotation(directionToPlayer);
            Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, alienRotation, rotationSpeed * Time.fixedDeltaTime);
            

            rb.MoveRotation(Quaternion.Euler(0, smoothRotation.eulerAngles.y, 0));
        }

        // Moves alien towards player
        Vector3 movement = transform.forward * walkSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
