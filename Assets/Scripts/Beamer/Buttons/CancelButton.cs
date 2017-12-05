using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		CurrentState.currentState = State.SELECTING;
	}
}
