using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
	public delegate void CollideAction(Direction direction);
	public static event CollideAction OnClicked;

	public delegate void CollideActionSelect();
	public static event CollideActionSelect OnClickedSelect;
	public static event CollideActionSelect OnDrinkTypeChange;

	public delegate void NFCScannedAction(int scanID);
	public static event NFCScannedAction OnNFCScanned;


	//TODO remove debug code
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 150, 5, 100, 30), "Left"))
		{
			if (OnClicked != null) {
				OnClicked (Direction.LEFT);
			}
		}
		if(GUI.Button(new Rect(Screen.width / 2 + 50, 5, 100, 30), "Select"))
		{
			if (OnClickedSelect != null) {
				OnClickedSelect ();
			}
		}
		if(GUI.Button(new Rect(Screen.width / 2 + 150, 5, 100, 30), "ScanNFC"))
		{
			if (OnClickedSelect != null) {
				ExecuteNFCScanned (9999);
			}
		}
	}

	public static void Execute (Direction dir) {
		if(OnClicked != null)
			OnClicked(dir);
	}

	public static void ExecuteSelect () {
		if(OnClickedSelect != null)
			OnClickedSelect();
	}

	public static void ExecuteNFCScanned(int nfcid) {
		if (OnNFCScanned != null)
			OnNFCScanned (nfcid);
	}

	public static void ExecuteDrinkTypeChange() {
		if (OnDrinkTypeChange != null)
			OnDrinkTypeChange ();
	}
}