
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;      //getting a call directly from the trigger and mapping to a variable
    Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;    //getting a call directly from the touchpad and mapping to a variable

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device controller;

    public GameObject hand;
    public GameObject mic;

    bool renderChange = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        /*EVENTS 
        //Trigger Mic -> Record
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);

        //Trigger Select
        GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerClicked);
        GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnclicked);

        //Touchpad Place
        GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
        GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
        */

    }

    void Update()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); //get integer representation of the controller

        if (controller.GetPressDown(triggerButton))
        {
            //SELECT
            Debug.Log("Trigger Pressed Once!");
        }
        else if(controller.GetPressUp(triggerButton))
        {
            renderChange = false;
            Debug.Log("Trigger Pressed Up!");
        }
        else if (controller.GetPress(triggerButton))
        {
            //RECORD AUDIO
            renderChange = true;
            Debug.Log("Trigger Pressed!");
            controller.TriggerHapticPulse(700);
        }
        else if (controller.GetPress(touchpadButton))
        {
            //PLACE
            Debug.Log("Touchpad Pressed Once!");
        }

        if(renderChange)
        {
            mic.SetActive(true);
            hand.SetActive(false);
            GetComponent<SteamVR_LaserPointer>().enabled = false;
        }
        else
        {
            mic.SetActive(false);
            hand.SetActive(true);
            GetComponent<SteamVR_LaserPointer>().enabled = true;
        }
    }


    /*
    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
                + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {

        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "pressed", e);
    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
    }

    private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "clicked", e);
    }

    private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
    {
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "unclicked", e);
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "pressed down", e);
    }

    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "released", e);
    }*/
} 
