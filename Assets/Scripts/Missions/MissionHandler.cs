using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    public GameObject missionObjectPrefab;

    public List<Mission> missions;

    private Mission currentMission;

    // Start is called before the first frame update
    void Start()
    {
        missions = new List<Mission>();

        Mission firstMission = new Mission(missionObjectPrefab, "Mission 1");
        firstMission.AddObjective("Collect rock sample");
        firstMission.AddObjective("Scan rock");

        currentMission = firstMission;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetCurrentMissionLog()
    {
        return currentMission.Log();
    }
}

