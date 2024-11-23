using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSideTrigger : MonoBehaviour
{
    public float sunRechargeRate = 2f;
    private PowerManager powerManager;
    private bool flashlightMessageShown;

    // Start is called before the first frame update
    void Start()
    {
        powerManager = GameObject.FindGameObjectWithTag("PowerManager").GetComponent<PowerManager>();
        flashlightMessageShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player is in light side");
            // Recharge power cuz of sun
            powerManager.AddUsage(-sunRechargeRate);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player is in dark side");
            // Stop recharging power
            powerManager.AddUsage(sunRechargeRate);
            if (!flashlightMessageShown) {
                GameObject.FindGameObjectWithTag("Message").GetComponent<MessageDisplay>().ShowMessage("Press F to toggle flashlight");
                flashlightMessageShown = true;
            }
        }
    }
}
