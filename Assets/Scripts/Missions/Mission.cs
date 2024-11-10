using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    private GameObject objectPrefab;
    private List<MissionObjective> missionObjectives;
    private string title;

    public Mission(GameObject objectPrefab, string title)
    {
        this.objectPrefab = objectPrefab;
        missionObjectives = new List<MissionObjective>();
        this.title = title;
    }

    public void AddObjective(string objective)
    {
        MissionObjective newObjective = new(objectPrefab, objective);
        missionObjectives.Add(newObjective);
    }

    public string Log()
    {
        string log = title;

        foreach (MissionObjective obj in missionObjectives)
        {
            log += "\n" + obj.GetObjectiveText();
        }

        return log;
    }
}