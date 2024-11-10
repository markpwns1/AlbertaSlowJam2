using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionObjective
{
    private GameObject gameObject;
    private string objective;
    private bool complete;
    private Vector3 objectLocation;

    public MissionObjective(GameObject prefab, string objective)
    {
        this.objective = objective;
        this.objectLocation = new(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        gameObject = MonoBehaviour.Instantiate(prefab, objectLocation, Quaternion.identity);
        MissionObjectBehaviour objectBehaviour = gameObject.GetComponent<MissionObjectBehaviour>();
        objectBehaviour.objective = this;
        this.complete = false;
    }

    public string GetObjectiveText()
    {
        string obj = (objective + " at (" + Math.Round(objectLocation.x) + ", " + Math.Round(objectLocation.z) + ")");
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
        GameObject.Destroy(gameObject);
    }

    public bool GetComplete()
    {
        return complete;
    }
}