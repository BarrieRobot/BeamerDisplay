using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (50, Screen.height / 2 - 50, 100, 30), "Left")) {
			EventManager.Execute (Direction.LEFT);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Left button triggered");
		EventManager.Execute (Direction.LEFT);
	}
}
