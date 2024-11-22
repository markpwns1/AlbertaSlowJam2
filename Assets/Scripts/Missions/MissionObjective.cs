using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionObjective
{
    private GameObject missionObject;
    private MissionObjectBehaviour objectBehaviour;
    private bool complete;
    private Vector3 objectLocation;

    public MissionObjective (GameObject missionObject) {
        this.missionObject = missionObject;
        this.objectBehaviour = missionObject.GetComponent<MissionObjectBehaviour>();
        this.objectLocation = missionObject.transform.position;
        this.complete = false;

        this.objectBehaviour.SetObjective(this);
        this.objectBehaviour.Activate();
    }

    public string GetObjectiveText()
    {
        string obj = (objectBehaviour.playerAction + " at (" + Math.Round(objectLocation.x) + ", " + Math.Round(objectLocation.z) + ")");
        // Add a strikethrough if it's finished
        if (complete)
        {
            return "<s>" + obj + "</s>";
        }
        return obj;
    }

    public void Fulfill ()
    {
        Debug.Log("Objective complete!");
        complete = true;
        if (objectBehaviour.disappearWhenComplete) {
            GameObject.Destroy(missionObject);
        }
    }

    public bool IsComplete()
    {
        return complete;
    }
}