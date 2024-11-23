using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
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

    public void ShowMessage(string message, bool urgent = false, float timeSeconds = 5f) {
        text.text = message;
        if (urgent) {
            text.color = Color.red;
        } else {
            text.color = Color.white;
        }
        text.enabled = true;

        StartCoroutine(RemoveMessage(timeSeconds));
    }

    public IEnumerator RemoveMessage (float delay) {
        // Wait for delay, then remove message
        yield return new WaitForSeconds(delay);
        text.enabled = false;
    }
}
