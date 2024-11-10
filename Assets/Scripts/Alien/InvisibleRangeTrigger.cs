using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleRangeTrigger : MonoBehaviour
{
    private AlienStalk alienStalk;

    // Start is called before the first frame update
    void Start()
    {
        alienStalk = GetComponentInParent<AlienStalk>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alienStalk.OnTriggerDetected(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alienStalk.OnTriggerDetected(false);
        }
    }
}
