using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    // public Transform player;
    new public CustomLight light;
    public float followSpeed = 5.0f;
    public float powerUsage = 1f;

    new private bool enabled = false;
    private PowerManager powerManager;

    void Start() {
        light.enabled = enabled;
        powerManager = GameObject.FindGameObjectWithTag("PowerManager").GetComponent<PowerManager>();
    }


    void Update() {
        if (powerManager.GetPowerLevel() > 0) {
            if(Input.GetKeyDown(KeyCode.F)) {
                enabled = !enabled;
                light.enabled = enabled;
                if (enabled) {
                    powerManager.AddUsage(powerUsage);
                } else {
                    powerManager.AddUsage(-powerUsage);
                }
            }
        } else {
            if (enabled) {
                // If light is still going when we run out of power, we need to reset the power usage
                powerManager.AddUsage(-powerUsage);
                enabled = false;
                light.enabled = false;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, followSpeed * Time.deltaTime);

        transform.position = Camera.main.transform.position;
    }
}
