using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public Image powerBar;
    
    private float powerLevel;
    private bool powerChanging;
    private float powerDelta;

    private readonly float MAXPOWER = 100f;

    // Start is called before the first frame update
    void Start()
    {
        powerLevel = MAXPOWER;
        powerDelta = 0;
        SetUsage(-5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerChanging) {
            powerLevel = Mathf.Clamp(powerLevel + powerDelta * Time.deltaTime, 0, MAXPOWER);
        }
        powerBar.rectTransform.localScale = new Vector3(5*(powerLevel / MAXPOWER), powerBar.rectTransform.localScale.y, powerBar.rectTransform.localScale.z);
    }

    public void SetUsage (float powerPerSec) {
        powerDelta = powerPerSec;
        powerChanging = true;
    }

    public void StopUsage () {
        powerChanging = false;
    }

    public void AddOrRemovePower (float power) {
        powerLevel = Mathf.Clamp(powerLevel + power, 0, MAXPOWER);
    }

    public float GetPowerLevel () {
        return powerLevel;
    }
}
