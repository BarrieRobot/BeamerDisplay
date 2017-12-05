using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFCDisplay : MonoBehaviour {
	Text text;
	public NFCValidator nfcvalidator;
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}

	void OnGUI() {
		if(GUI.Button(new Rect(Screen.width / 2 + 150, 5, 100, 30), "ScanNFC"))
		{
			EventManager.ExecuteNFCScanned (9999);
		}
	}

	// Update is called once per frame
	void Update () {
		if (nfcvalidator.GetNFCID() != null)
			text.text = "NFC ID=" + nfcvalidator.GetNFCID();
	}}
