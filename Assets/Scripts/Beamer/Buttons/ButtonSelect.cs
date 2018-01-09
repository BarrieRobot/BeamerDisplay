using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{

	//TODO remove debug code
	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 30), "Select")) {
			OnTriggerEnter2D (null);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			EventManager.ExecuteSelect ();
		}
	}
}
