using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeScript : MonoBehaviour
{
    private AttackState attackState;

    // Start is called before the first frame update
    void Start()
    {
        attackState = GetComponentInParent<AttackState>();
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
            attackState.OnAttackTriggerDetected(true);
        }
    }
    private void OnTriggerExt(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attackState.OnAttackTriggerDetected(false);
        }
    }

}
