using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFCValidator : MonoBehaviour
{
	public UDPSend udpsender;
	public ItemSelector itemSelector;
	public DatabaseManager databaseManager;
	public float NFCTimeoutSeconds;
	public GameObject ResetProgressDisplay;
	private int currentActiveNFCID;
	public float inactivityTimer = 0;
	private float ProgressbarFractionOfResetTime = 4;

	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width / 2 - 120, Screen.height - 150, 80, 100), "ColdDrink")) {
			/*if (CurrentState.currentState == State.CHOOSING_CATEGORY) {
				EventManager.ExecuteCategoryChosen ();
			}*/
			CurrentState.currentState = State.SELECTING;
			CurrentState.drink = DrinkType.COLD;
			EventManager.ExecuteDrinkTypeChange ();
		}
		if (GUI.Button (new Rect (Screen.width / 2, Screen.height - 150, 80, 100), "HotDrink")) {
			/*if (CurrentState.currentState == State.CHOOSING_CATEGORY) {
				EventManager.ExecuteCategoryChosen ();
			}*/
			CurrentState.currentState = State.SELECTING;
			CurrentState.drink = DrinkType.HOT;
			EventManager.ExecuteDrinkTypeChange ();
		}
	}


	// Use this for initialization
	void Start ()
	{
		EventManager.OnNFCScanned += OnScanned;
		EventManager.OnAnything += ResetNFCInactivity;
	}

	void OnEnable ()
	{
		EventManager.OnNFCScanned += OnScanned;
		EventManager.OnAnything += ResetNFCInactivity;
	}

	void OnDisable ()
	{
		EventManager.OnNFCScanned -= OnScanned;
		EventManager.OnAnything -= ResetNFCInactivity;
	}

	void ResetNFCInactivity ()
	{
		ResetProgressDisplay.SetActive (false);
		ResetProgressDisplay.GetComponent <Image> ().fillAmount = 0;
		inactivityTimer = 0;
	}

	void OnScanned (int scannedID)
	{
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			currentActiveNFCID = scannedID;
			CurrentState.currentState = State.CHOOSING_CATEGORY;
			databaseManager.getExistingOrders (scannedID);
			break;
		case State.CONFIRMING:
			if (scannedID == currentActiveNFCID) { //TODO change to CurrentSelection
				udpsender.SendString ("order: " + CurrentSelection.selectionid);
				databaseManager.insertOrder (scannedID, CurrentSelection.selectionid, CurrentSelection.selectionname);
				CurrentState.currentState = State.PREPARING;
			} else {
				Debug.Log ("CONFIRM FAILED");
			}
			break;
		default:
			break;
		}
	}

	public int GetNFCID ()
	{
		return currentActiveNFCID;
	}

	void Update ()
	{
		if (CurrentState.currentState != State.WAIT_FOR_NFC && CurrentState.currentState != State.PREPARING) {
			inactivityTimer += Time.deltaTime;
		}
		if (inactivityTimer > NFCTimeoutSeconds - NFCTimeoutSeconds / ProgressbarFractionOfResetTime) {
			ResetProgressDisplay.SetActive (true);
			ResetProgressDisplay.GetComponent <Image> ().fillAmount = (inactivityTimer - NFCTimeoutSeconds * ((ProgressbarFractionOfResetTime - 1) / ProgressbarFractionOfResetTime)) / (NFCTimeoutSeconds / ProgressbarFractionOfResetTime);
		}
		if (inactivityTimer > NFCTimeoutSeconds) {
			currentActiveNFCID = 0;
			inactivityTimer = 0;
			ResetProgressDisplay.GetComponent <Image> ().fillAmount = 0;
			ResetProgressDisplay.SetActive (false);
			CurrentState.currentState = State.WAIT_FOR_NFC;
		}
	}
}
