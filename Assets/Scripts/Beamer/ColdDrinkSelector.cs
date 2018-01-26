using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDrinkSelector : MonoBehaviour
{
	public Animator animator;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback") && animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {
			/*	if (CurrentState.currentState == State.CHOOSING_CATEGORY) {
				EventManager.ExecuteCategoryChosen ();
			}*/
			Debug.Log ("Cold drink triggered");
			CurrentState.currentState = State.SELECTING;
			CurrentState.drink = DrinkType.COLD;
			EventManager.ExecuteDrinkTypeChange ();
		}
	}

	//TODO remove debug code
	void OnGUI ()
	{
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
