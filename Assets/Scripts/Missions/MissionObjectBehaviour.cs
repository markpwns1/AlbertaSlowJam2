using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjectBehaviour : MonoBehaviour {
    public string playerAction;
    public string promptText;
    public bool disappearWhenComplete;
    public bool scannable;
    public float scanPowerUsage = 15f;

    private PowerManager power;
    private MissionObjective objective;
    private Interacter interacter;

    // Start is called before the first frame update
    void Start() {
        interacter = new Interacter(this.gameObject, OnInteract, promptText);
        interacter.SetInteractable(false);

        power = GameObject.FindGameObjectWithTag("PowerManager").GetComponent<PowerManager>();
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
        if (scannable) {
            if (power.GetPowerLevel() < scanPowerUsage) {
                GameObject.FindGameObjectWithTag("Message").GetComponent<MessageDisplay>().ShowMessage("Not enough power", true, 1.5f);
                return;
            }
            power.AddOrRemovePower(-scanPowerUsage);
        }
        objective.Fulfill();
        interacter.SetInteractable(false);
    }
}
