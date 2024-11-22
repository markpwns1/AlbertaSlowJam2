using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectBehaviour : MonoBehaviour {
    public string playerAction;
    public string promptText;
    public bool disappearWhenComplete;

    private MissionObjective objective;
    private Interacter interacter;

    // Start is called before the first frame update
    void Start() {
        interacter = new Interacter(this.gameObject, OnInteract, promptText);
        interacter.SetInteractable(false);
    }

    // Update is called once per frame
    void Update() {
        interacter.Update();
    }

    public void SetObjective(MissionObjective objective) {
        this.objective = objective;
    }

    public void Activate() {
        interacter.SetInteractable(true);
    }

    private void OnInteract() {
        objective.Fulfill();
        interacter.SetInteractable(false);
    }
}
