using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoonbaseDoor : MonoBehaviour
{
    private Interacter interacter;

    // Start is called before the first frame update
    void Start()
    {
        interacter = new Interacter(this.gameObject, OnInteract, "Press E to end the day", interactKey: KeyCode.E);
    }

    // Update is called once per frame
    void Update()
    {
        interacter.Update();
    }

    void OnInteract() {
        Debug.Log("Interacting with door");
        SceneManager.LoadScene("DayChangeScene");
    }
}
