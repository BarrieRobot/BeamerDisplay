using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onEnable () {

	}

	void onDisable() {

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Right button triggered");

	}
}
