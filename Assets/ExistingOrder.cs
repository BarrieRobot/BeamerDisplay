using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExistingOrder : MonoBehaviour
{

	public Text drinknametext;
	public int drinkid;
	public DrinkType drinktype;

	private string drinkname;

	public void setDrinkID (int drinkid)
	{
		this.drinkid = drinkid;
	}

	public void setDrinkName (string drinkname)
	{
		this.drinkname = drinkname;
	}

	public void setDrinkType (DrinkType drinktype)
	{
		this.drinktype = drinktype;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains ("Feedback")) {
			Debug.Log ("drinkid= " + drinkid);
			CurrentSelection.selectionid = drinkid;
			CurrentSelection.selectionname = drinkname;
			CurrentState.drink = drinktype;
			if (drinknametext != null)
				drinknametext.text = drinkname;
			CurrentState.currentState = State.CONFIRMING;
			EventManager.ExecuteDrinkTypeChange ();
		}
	}
}
