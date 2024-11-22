using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    private List<MissionObjective> missionObjectives;
    private string title;

    public Mission(string title)
    {
        missionObjectives = new List<MissionObjective>();
        this.title = title;
    }

    public void AddObjective(GameObject missionObject) {
        missionObjectives.Add(new MissionObjective(missionObject));
    }

    public bool IsComplete() {
        bool res = true;
        foreach (MissionObjective obj in missionObjectives) {
            if (!obj.IsComplete()) {
                res = false;
            }
        }
        return res;
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