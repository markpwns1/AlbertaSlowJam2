using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonbaseDoor : MonoBehaviour
{
    private Interacter interacter;

    // Start is called before the first frame update
    void Start()
    {
        interacter = new Interacter(this.gameObject, OnInteract, "Press E to end the day", interactKey: KeyCode.F);
    }

    // Update is called once per frame
    void Update()
    {
        interacter.Update();
    }

    void OnInteract() {
        Debug.Log("Door interacted with!");
        interacter.SetInteractable(false);
    }
}
