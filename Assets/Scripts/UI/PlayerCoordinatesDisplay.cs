using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoordinatesDisplay : MonoBehaviour
{
    public GameObject player;
    public TMP_Text coordsText;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerCoords = player.transform.position;
        coordsText.text = Math.Round(playerCoords.x) + ", " + Math.Round(playerCoords.z); 
    }
}
