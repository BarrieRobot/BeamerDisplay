using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDisplay : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" + CurrentState.currentState;
	}
}
