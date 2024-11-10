using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkState : State
{
    public TeleportState teleportState;
    public HuntState huntState;
    public bool inTeleportState = false;
    public bool inHuntState = false;
    public override State RunCurrentState()
    {
        if (inTeleportState)
        {
            return teleportState;
        } else if (inHuntState)
        {
            return huntState;
        } else
        {
            return this;
        }
    }
}
