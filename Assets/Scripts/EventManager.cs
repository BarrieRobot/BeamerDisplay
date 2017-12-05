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