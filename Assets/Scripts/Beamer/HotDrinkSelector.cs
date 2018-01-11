using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDrinkSelector : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			if (CurrentState.currentState == State.CHOOSING_CATEGORY) {
				EventManager.ExecuteCategoryChosen ();
			}
			Debug.Log ("Hot drink triggered");
			CurrentState.currentState = State.SELECTING;
			CurrentState.drink = DrinkType.HOT;
			EventManager.ExecuteDrinkTypeChange ();
		}
	}

	//TODO remove debug code
	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width / 2, Screen.height - 150, 80, 100), "HotDrink")) {
			if (CurrentState.currentState == State.CHOOSING_CATEGORY) {
				EventManager.ExecuteCategoryChosen ();
			}
			CurrentState.currentState = State.SELECTING;
			CurrentState.drink = DrinkType.HOT;
			EventManager.ExecuteDrinkTypeChange ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
