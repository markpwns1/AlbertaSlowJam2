using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLight : MonoBehaviour
{
    public bool isSpotLight = true;
    public float range = 100.0f;
    public float angle = 30.0f;
    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetDirection() {
        return transform.forward;
    }
}
