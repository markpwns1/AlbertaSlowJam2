using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectBehaviour : MonoBehaviour
{
    public float interactionRange = 5f;
    public KeyCode interactKey = KeyCode.E;
    public MissionObjective objective;

    private Transform playerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.transform == transform)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    if (objective != null)
                    {
                        objective.Fulfill();
                    }
                }
            }   
        }
    }
}
