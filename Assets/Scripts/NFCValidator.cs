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
		inactivityTimer = 0;
	}

	void OnScanned (int scannedID)
	{
		Debug.Log ("Onscanned NFCValidator");
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			currentActiveNFCID = scannedID;
			CurrentState.currentState = State.CHOOSING_CATEGORY;
			break;
		case State.CONFIRMING:
			if (scannedID == currentActiveNFCID) {
				udpsender.SendString ("order placed: " + itemSelector.getCurrentItem ().name);
				databaseManager.insertOrder (scannedID, itemSelector.getCurrentItem ().drink, itemSelector.getCurrentItem ().name);
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
		if (CurrentState.currentState != State.WAIT_FOR_NFC) {
			inactivityTimer += Time.deltaTime;
		}
		if (inactivityTimer > NFCTimeoutSeconds / 2) {
			ResetProgressDisplay.SetActive (true);
			ResetProgressDisplay.GetComponent <Image> ().fillAmount = (inactivityTimer - NFCTimeoutSeconds / 2) / (NFCTimeoutSeconds / 2);
		}
		if (inactivityTimer > NFCTimeoutSeconds) {
			Debug.Log ("reset nfc");
			currentActiveNFCID = 0;
			inactivityTimer = 0;
			ResetProgressDisplay.GetComponent <Image> ().fillAmount = 0;
			ResetProgressDisplay.SetActive (false);
			CurrentState.currentState = State.WAIT_FOR_NFC;
		}
	}
}
