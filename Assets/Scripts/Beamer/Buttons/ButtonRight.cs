using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width - 150, Screen.height / 2 - 50, 100, 30), "Right")) {
			EventManager.Execute (Direction.RIGHT);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Right button triggered");
		EventManager.Execute (Direction.RIGHT);
	}
}
