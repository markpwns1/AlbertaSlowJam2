using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectBehaviour : MonoBehaviour {
    public MissionObjective objective;

    private Interacter interacter;

    // Start is called before the first frame update
    void Start() {
        interacter = new Interacter(this.gameObject, OnInteract, "Press E to interact");
    }

    // Update is called once per frame
    void Update() {
        interacter.Update();
    }

    private void OnInteract() {
        objective.Fulfill();
    }
}
