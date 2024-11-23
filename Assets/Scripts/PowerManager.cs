using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public Image powerBar;
   
    private float powerDelta;

    private readonly float MAXPOWER = 100f;
    public float powerLevel = 100f;

    // Start is called before the first frame update
    void Start()
    {
        powerDelta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        powerLevel = Mathf.Clamp(powerLevel + powerDelta * Time.deltaTime, 0, MAXPOWER);
        powerBar.rectTransform.localScale = new Vector3(5*(powerLevel / MAXPOWER), powerBar.rectTransform.localScale.y, powerBar.rectTransform.localScale.z);
    }

    public void AddUsage (float powerPerSec) {
        // Positive usage = negative power delta
        powerDelta -= powerPerSec;
    }

    public void AddOrRemovePower (float power) {
        powerLevel = Mathf.Clamp(powerLevel + power, 0, MAXPOWER);
    }

    public float GetPowerLevel () {
        return powerLevel;
    }
}
