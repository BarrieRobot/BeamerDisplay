using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Left button triggered");
		EventManager.Execute (Direction.LEFT);
	}
}
