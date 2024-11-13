using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTeleport : MonoBehaviour
{
    private Renderer alienRender;
    public Transform player;
    public float teleportOffset = 75f;
    public bool isPlayerTooFar = false;

    
    // Start is called before the first frame update
    void Start()
    {
        alienRender = GetComponent<Renderer>();
        EnableInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);

        if (isPlayerTooFar)
        {
            TeleportToPlayer();
            isPlayerTooFar = false;
        }
    }
    private void TeleportToPlayer()
    {
        Debug.Log("Alien Creeps closer");
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        Vector3 newPosition = player.position + directionToPlayer * teleportOffset;
        transform.position = newPosition;
    }
    public void EnableInvisible()
    {
        alienRender.enabled = false;
    }
    public void DisableInvisible()
    {
        alienRender.enabled = true;
    }

    public void OnTriggerDetected(bool isPlayerOutsideRange)
    {
        if (isPlayerOutsideRange)
        {
            isPlayerTooFar = true;
        } else if (!isPlayerOutsideRange)
        {
            isPlayerTooFar = false;
        }

    }
}
