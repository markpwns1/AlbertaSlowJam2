using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    public GameObject missionObjectPrefab;
    public GameObject moonbaseDoor;

    public List<Mission> missions;

    private Mission currentMission;
    private bool doorOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMission = new Mission("Mission " + SharedData.gameDay);
        GameObject[] missionObjects = GameObject.FindGameObjectsWithTag("Mission" + SharedData.gameDay);
        foreach (GameObject missionObject in missionObjects) {
            currentMission.AddObjective(missionObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Allow user to end the day at the moonbase if mission is complete
        if (!doorOpened && currentMission.IsComplete()) {
            moonbaseDoor.GetComponent<MoonbaseDoor>().Activate();
            doorOpened = true;
        }
    }

    public string GetCurrentMissionLog()
    {
        return currentMission.Log();
    }
}

