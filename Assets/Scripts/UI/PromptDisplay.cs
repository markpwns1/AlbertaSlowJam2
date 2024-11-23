using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptDisplay : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisplayPrompt (string interactText)
    {
        text.text = interactText;
        text.enabled = true;
    }

    public void RemovePrompt () {
        text.enabled = false;
    }
}