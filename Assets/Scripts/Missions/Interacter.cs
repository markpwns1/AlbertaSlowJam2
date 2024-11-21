
using System;
using UnityEngine;
using UnityEngine.UIElements;

/* A class that allows a gameobject to be interacted with, displaying a prompt when the player looks at it */
public class Interacter {
    private KeyCode interactKey;
    private float interactionRange;

    private Transform playerCamera;
    private PromptDisplay promptDisplay;
    private GameObject interactionObject;
    private string promptText;
    private Action onInteract;
    private bool promptDisplayed = false;
    private bool interactable = true;

    public Interacter(GameObject interactionObject, Action onInteract, string promptText, KeyCode interactKey = KeyCode.E, float interactionRange = 5f) {
        this.playerCamera = Camera.main.transform;
        this.promptDisplay = GameObject.FindGameObjectWithTag("Prompt").GetComponent<PromptDisplay>();

        this.interactionObject = interactionObject;
        this.promptText = promptText;
        this.onInteract = onInteract;

        this.interactKey = interactKey;
        this.interactionRange = interactionRange;
    }

    /* Enables or disables player interaction with the object */
    public void SetInteractable (bool interactable) {
        this.interactable = interactable;
    }

    /* Call in the update method of any gameObject that has an interacter */
    public void Update () {
        if (interactable) {
            RaycastHit hit;
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);

            if (Physics.Raycast(ray, out hit, interactionRange)) {
                if (hit.transform == interactionObject.transform) {
                    DisplayPrompt();
                    if (Input.GetKeyDown(interactKey)) {
                        onInteract();
                        RemovePrompt();
                    }
                } else {
                    RemovePrompt();
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
            promptDisplay.DisplayPrompt(promptText);
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
