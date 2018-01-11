using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramHighlighter : MonoBehaviour
{

	public GameObject show;
	
	// Update is called once per frame
	void Update ()
	{
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			show.SetActive (true);
			//chooseDrinkpanel.SetActive (false);
			//coffee.SetActive (false);
			//soda.SetActive (false);
			break;
		case State.CHOOSING_CATEGORY:
			show.SetActive (true);
			//chooseDrinkpanel.SetActive (true);
			break;
		default:
			//show.SetActive (false);
			break;
		}
	}
}
