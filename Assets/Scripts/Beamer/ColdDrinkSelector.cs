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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
