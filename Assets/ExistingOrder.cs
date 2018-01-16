using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExistingOrder : MonoBehaviour
{

	public Text drinknametext;
	public int drinkid;

	private string drinkname;

	public void setDrinkID (int drinkid)
	{
		this.drinkid = drinkid;
	}

	public void setDrinkName (string drinkname)
	{
		this.drinkname = drinkname;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			CurrentSelection.selectionid = drinkid;
			CurrentSelection.selectionname = drinkname;
			if (drinknametext != null)
				drinknametext.text = drinkname;
			CurrentState.currentState = State.CONFIRMING;
		}
	}
}
