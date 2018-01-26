using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : MonoBehaviour
{
	float touchedTime = 0;
	bool touch = false;

	void FixedUpdate ()
	{
		if (touch) {
			touchedTime += Time.fixedDeltaTime;
			if (touchedTime > 0.1) {
				touch = false;
				SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
				renderer.color = new Color (1f, 1f, 1f, 1f); 
				EventManager.Execute (Direction.LEFT);
			}
		}
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (50, 5, 100, 30), "Left")) {
			EventManager.Execute (Direction.LEFT);
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

	/*void OnTriggerExit2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			touchedTime += Time.deltaTime;
			if (touchedTime > 0.1) {
				SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
				renderer.color = new Color (1f, 1f, 1f, 1f); 
			}
		}
	}*/
}
