using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private State currentState;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device rightHand;

    public GameObject hand;
    public GameObject mic;

    private void Start ()
    {
        SetState(new defaultState(this));
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	private void Update ()
    {
        currentState.Tick(trackedObj, rightHand, hand, mic);
	}

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        gameObject.name = "Cube - " + state.GetType().Name;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
