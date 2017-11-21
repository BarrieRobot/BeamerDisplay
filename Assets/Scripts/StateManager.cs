using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StateManager : MonoBehaviour {

	public UDPReceive udpreceiver;

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

	void ReceiveStateChange() {
		if (udpreceiver != null) {
			int rfidid = udpreceiver.getLastReceivedRFID ();
			if (rfidid != null && rfidid != -1) {
				CurrentState.currentState = State.SELECTING;
			} else {
				CurrentState.currentState = State.WAIT_FOR_NFC;
			}
		}
	}
}
