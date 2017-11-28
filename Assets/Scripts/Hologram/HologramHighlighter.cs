using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramHighlighter : MonoBehaviour {

	public GameObject scanTagPanel;
	public GameObject chooseDrinkpanel;

	public GameObject coffee;
	public GameObject soda;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			scanTagPanel.SetActive (true);
			chooseDrinkpanel.SetActive (false);
			//coffee.SetActive (false);
			//soda.SetActive (false);
			break;
		case State.CHOOSING_CATEGORY:
			scanTagPanel.SetActive (false);
			chooseDrinkpanel.SetActive (true);
			break;
		case State.SELECTING:
			scanTagPanel.SetActive (false);
			chooseDrinkpanel.SetActive (false);
			//soda.SetActive (true);
			break;
		default:
			break;
		}
	}
}
