using System;
using System.Collections;
using UnityEngine;

public class RightHandCases : MonoBehaviour
{
    enum GameState {defaultState, recordingState}
    private GameState currentState;

    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;      //getting a call directly from the trigger and mapping to a variable
    Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;    //getting a call directly from the touchpad and mapping to a variable

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device controller;

    public GameObject hand;
    public GameObject mic;

    bool renderChange = false;
    bool isCollideDock = false;
    bool isRaycastMenu = false;
    bool isRaycastNote = false;

    void Start ()
    {
        SetCurrentState(GameState.defaultState);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update ()
    {
        switch(currentState)
        {
            case GameState.defaultState:
                controller = SteamVR_Controller.Input((int)trackedObj.index); //get integer representation of the controller

                if (controller.GetPress(triggerButton))
                {
                    Debug.Log("Select!");
                }
                else if (controller.GetPress(touchpadButton))
                {
                    Debug.Log("You have placed the note");
                }
                break;

            case GameState.recordingState:
                controller = SteamVR_Controller.Input((int)trackedObj.index); //get integer representation of the controller

                if (controller.GetPressUp(triggerButton))
                {
                    renderChange = false;
                    Debug.Log("Stopped Recording!");
                }
                else if (controller.GetPress(triggerButton))
                {
                    renderChange = true;
                    Debug.Log("Recording!");
                    controller.TriggerHapticPulse(700);
                }
                else if (controller.GetPress(touchpadButton))
                {
                    Debug.Log("You have placed the note");
                }

                if (renderChange)
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
                break;
        }
    }

    private void SetCurrentState(GameState state)
    {
        currentState = state;
    }
}
