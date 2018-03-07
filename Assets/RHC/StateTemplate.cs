using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected RightHand controller;

    public abstract void Tick(SteamVR_TrackedObject trackedObj, SteamVR_Controller.Device rightHand, GameObject hand, GameObject mic);
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(RightHand state)
    {
        this.controller = state;
    }
}
