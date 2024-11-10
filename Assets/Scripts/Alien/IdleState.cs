using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public TeleportState teleportState;
    public bool canSeePlayer;
    public override State RunCurrentState()
    {
        return this;
    }

}
