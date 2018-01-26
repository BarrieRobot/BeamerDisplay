using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewOrder : MonoBehaviour
{

	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width - 150, 5, 100, 30), "NEW")) {
			CurrentState.currentState = State.CHOOSING_CATEGORY;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			CurrentState.currentState = State.CHOOSING_CATEGORY;
		}
	}
}
