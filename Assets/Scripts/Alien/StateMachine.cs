using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;


    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null && nextState != currentState )
        {
            SwitchToNextState( nextState );
        }
    }
    private void SwitchToNextState( State nextState )
    {
        currentState = nextState;
    }

}
