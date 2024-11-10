using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionLogDisplay : MonoBehaviour
{
    public GameObject missionHandlerObject;
    public TMP_Text missionLogText;

    // Update is called once per frame
    void Update()
    {
        MissionHandler missionHandler = missionHandlerObject.GetComponent<MissionHandler>();
        missionLogText.text = missionHandler.GetCurrentMissionLog();
    }
}
