using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeScript : MonoBehaviour
{
    private HuntState huntState;

    // Start is called before the first frame update
    void Start()
    {
        huntState = GetComponentInParent<HuntState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered attack range");
            huntState.OnAttackTriggerDetected(true);
        }
    }
    private void OnTriggerExt(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            huntState.OnAttackTriggerDetected(false);
        }
    }

}
