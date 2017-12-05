using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDrinkSelector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Cold drink triggered");
		CurrentState.currentState = State.SELECTING;
		CurrentState.drink = DrinkType.COLD;
		EventManager.ExecuteDrinkTypeChange ();
	}

	//TODO remove debug code
	void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width / 2 - 120, Screen.height-150, 80, 100), "ColdDrink")) {
			OnTriggerEnter2D (null);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
