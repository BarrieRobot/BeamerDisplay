using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDrinkSelector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Hot drink triggered");
		CurrentState.currentState = State.SELECTING;
		CurrentState.drink = Drink.HOT;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
