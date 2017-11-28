using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFCValidator : MonoBehaviour {

	private int currentActiveNFCID;

	// Use this for initialization
	void Start () {
		EventManager.OnNFCScanned += OnScanned;
	}

	void OnEnable() {
		EventManager.OnNFCScanned += OnScanned;
	}

	void OnDisable() {
		EventManager.OnNFCScanned -= OnScanned;
	}

	void OnScanned(int scannedID) {
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			currentActiveNFCID = scannedID;
			CurrentState.currentState = State.CHOOSING_CATEGORY;
			break;
		case State.CONFIRMING:
			if (scannedID == currentActiveNFCID) {
				Debug.Log ("CONFIRMED");
				CurrentState.currentState = State.PREPARING;
			} else {
				Debug.Log ("CONFIRM FAILED");
			}
			break;
		default:
			break;
		}
	}
}
