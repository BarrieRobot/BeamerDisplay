using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramHighlighter : MonoBehaviour
{

	public GameObject show;
	public GameObject minigame;
	
	// Update is called once per frame
	void Update ()
	{
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			DisableItems ();
			show.SetActive (true);
			//chooseDrinkpanel.SetActive (false);
			//coffee.SetActive (false);
			//soda.SetActive (false);
			break;
		case State.CHOOSING_CATEGORY:
			DisableItems ();
			show.SetActive (true);
			//chooseDrinkpanel.SetActive (true);
			break;
		case State.PREPARING:
			DisableItems ();
			minigame.SetActive (true);
			break;
		default:
			DisableItems ();
			//show.SetActive (false);
			break;
		}
	}

	public void DisableItems ()
	{
		minigame.SetActive (false);
	}
}
