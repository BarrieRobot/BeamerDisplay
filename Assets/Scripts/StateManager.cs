using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StateManager : MonoBehaviour {

	public UDPReceive udpreceiver;
	public TouchInterpreter touchInput;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		ReceiveStateChange ();
	}

	void HandleState () {
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			break;
		case State.SELECTING:
			break;
		}
	}

	public void ChangeState(State newState) {
		CurrentState.currentState = newState;
	}

	void ReceiveStateChange() {
		if (udpreceiver != null && CurrentState.currentState == State.WAIT_FOR_NFC) {
			int rfidid = udpreceiver.getLastReceivedRFID ();
			if (rfidid != null && rfidid != -1) {
				Debug.Log ("State manager switching to CHOOSING_CATEGORY");
				CurrentState.currentState = State.CHOOSING_CATEGORY;
			} else {
				CurrentState.currentState = State.WAIT_FOR_NFC;
			}
		}
	}
}
