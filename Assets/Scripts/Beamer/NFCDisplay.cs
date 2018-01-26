using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFCDisplay : MonoBehaviour
{
	Text text;
	public NFCValidator nfcvalidator;
	// Use this for initialization
	void Start ()
	{
		text = gameObject.GetComponent<Text> ();
	}

	void OnGUI ()
	{
		if (CurrentState.currentState == State.WAIT_FOR_NFC || CurrentState.currentState == State.CONFIRMING) {
			if (GUI.Button (new Rect (Screen.width / 2, 5, 100, 30), "ScanNFC")) {
				EventManager.ExecuteNFCScanned (9999);
			}
		}
		/*if (GUI.Button (new Rect (Screen.width / 2 - 150, 5, 100, 30), "ScanNFC2")) {
			//EventManager.ExecuteNFCScanned (999);
			CurrentState.currentState = State.COMPLETED;
		}*/
	}

	// Update is called once per frame
	void Update ()
	{
		if (nfcvalidator.GetNFCID () != null)
			text.text = "NFC ID=" + nfcvalidator.GetNFCID ();
	}
}
