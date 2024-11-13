using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    // public Transform player;
    new public CustomLight light;
    public float followSpeed = 5.0f;

    new private bool enabled = false;

    void Start() {
        light.enabled = enabled;
    }


    void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            enabled = !enabled;
            light.enabled = enabled;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, followSpeed * Time.deltaTime);

        transform.position = Camera.main.transform.position;
    }
}
