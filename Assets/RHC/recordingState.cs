using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordingState : State
{
    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;      //getting a call directly from the trigger and mapping to a variable
    Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;    //getting a call directly from the touchpad and mapping to a variable

    bool renderChange = false;

    public recordingState(RightHand controller) : base(controller)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("You are on the recording state right now!");
    }

    public override void Tick(SteamVR_TrackedObject trackedObj, SteamVR_Controller.Device rightHand, GameObject hand, GameObject mic)
    {
        rightHand = SteamVR_Controller.Input((int)trackedObj.index); //get integer representation of the controller

        if (rightHand.GetPressUp(triggerButton))
        {
            renderChange = false;
            Debug.Log("Stopped Recording!");
        }
        else if (rightHand.GetPress(triggerButton))
        {
            renderChange = true;
            Debug.Log("Recording!");
            rightHand.TriggerHapticPulse(700);
        }
        else if (rightHand.GetPress(touchpadButton))
        {
            Debug.Log("You have placed the note");
        }

        if (renderChange)
        {
            mic.SetActive(true);
            hand.SetActive(false);
        }
        else
        {
            mic.SetActive(false);
            hand.SetActive(true);
        }
    }

}

