using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewOrder : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width - 150, Screen.height / 2, 100, 30), "NEW")) {
			OnTriggerEnter2D(null);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		CurrentState.currentState = State.CHOOSING_CATEGORY;
	}
}
