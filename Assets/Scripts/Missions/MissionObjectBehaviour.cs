using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectBehaviour : MonoBehaviour {
    public float interactionRange = 5f;
    public KeyCode interactKey = KeyCode.E;
    public MissionObjective objective;

    private Transform playerCamera;
    private PromptDisplay promptDisplay;
    private bool promptDisplayed = false;
    
    // Start is called before the first frame update
    void Start() {
        playerCamera = Camera.main.transform;
        promptDisplay = GameObject.FindGameObjectWithTag("Prompt").GetComponent<PromptDisplay>();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out hit, interactionRange)) {
            if (hit.transform == transform) {
                DisplayPrompt();
                if (Input.GetKeyDown(interactKey)) {
                    if (objective != null) {
                        RemovePrompt();
                        objective.Fulfill();
                    }
                }
            } else {
                RemovePrompt();
            }
        } else {
            RemovePrompt();
        }
    }

    void DisplayPrompt() {
        if (!promptDisplayed) {
            promptDisplay.DisplayPrompt("Press E to scan");
        }
        promptDisplayed = true;
    }

    void RemovePrompt() {
        if (promptDisplayed) {
            promptDisplay.RemovePrompt();
        }
        promptDisplayed = false;
    }
}
