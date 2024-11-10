using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRangeTrigger : MonoBehaviour
{
    private AlienTeleport alienTeleport;
    // Start is called before the first frame update
    void Start()
    {
        alienTeleport = GetComponentInParent<AlienTeleport>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alienTeleport.OnTriggerDetected(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alienTeleport.OnTriggerDetected(true);
        }
    }
}
