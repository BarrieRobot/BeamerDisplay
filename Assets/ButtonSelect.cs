using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("select button triggered");
		EventManager.ExecuteSelect ();
	}
}
