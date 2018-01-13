using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public delegate void CollideActionDirection (Direction direction);

	public static event CollideActionDirection OnClicked;

	public delegate void CollideActionSelect ();

	public static event CollideActionSelect OnClickedSelect;
	public static event CollideActionSelect OnDrinkTypeChange;

	public delegate void NFCScannedAction (int scanID);

	public static event NFCScannedAction OnNFCScanned;

	public delegate void AnyAction ();

	public static event AnyAction OnAnything;

	public delegate void StartMinigameAction ();

	public static event StartMinigameAction OnStartMinigame;

	public delegate void CategoryChosenAction ();

	public static event CategoryChosenAction OnCategoryChosen;

	public delegate void ShowOverAction ();

	public static event ShowOverAction OnShowOver;

	//TODO remove debug code
	void OnGUI ()
	{

	}

	public static void Execute (Direction dir)
	{
		ExecuteAnyAction ();
		if (OnClicked != null)
			OnClicked (dir);
	}

	public static void ExecuteSelect ()
	{
		ExecuteAnyAction ();
		if (OnClickedSelect != null)
			OnClickedSelect ();
	}

	public static void ExecuteNFCScanned (int nfcid)
	{
		ExecuteAnyAction ();
		if (OnNFCScanned != null)
			OnNFCScanned (nfcid);
	}

	public static void ExecuteDrinkTypeChange ()
	{
		ExecuteAnyAction ();
		if (OnDrinkTypeChange != null)
			OnDrinkTypeChange ();
	}

	public static void ExecuteAnyAction ()
	{
		if (OnAnything != null)
			OnAnything ();
	}

	public static void ExecuteStartMinigame ()
	{
		if (OnStartMinigame != null)
			OnStartMinigame ();
	}

	public static void ExecuteCategoryChosen ()
	{
		if (OnCategoryChosen != null)
			OnCategoryChosen ();
	}


	public static void ExecuteShowOver ()
	{
		if (OnShowOver != null)
			OnShowOver ();
	}
}