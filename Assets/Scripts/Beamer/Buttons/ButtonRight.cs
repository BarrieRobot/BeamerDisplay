using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : MonoBehaviour
{

	float touchedTime = 0;
	bool touch = false;


	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width - 150, Screen.height / 2 - 50, 100, 30), "Right")) {
			EventManager.Execute (Direction.RIGHT);
		}
	}

	void FixedUpdate ()
	{
		if (touch) {
			touchedTime += Time.fixedDeltaTime;
			if (touchedTime > 0.1) {
				touch = false;
				SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
				renderer.color = new Color (1f, 1f, 1f, 1f); 
				EventManager.Execute (Direction.RIGHT);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			touchedTime = 0;
			touch = true;
			SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
			renderer.color = new Color (0.5f, 0.5f, 0.5f, 1f); // Set to opaque gray
		}
	}
}
