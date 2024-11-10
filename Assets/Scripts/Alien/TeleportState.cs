using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    public StalkState stalkState;
    public bool inStalkState = false;
    public override State RunCurrentState()
    {
        if (inStalkState)
        {
            return stalkState;
        } else
        {
            return this;
        }
    }
}
