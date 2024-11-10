using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    public TeleportState teleportState;
    public bool inTeleportState;
    public override State RunCurrentState()
    {
        if (inTeleportState)
        {
            return teleportState;
        } else
        {
            return this;
        }
    }
}
