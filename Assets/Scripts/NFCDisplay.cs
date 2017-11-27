using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFCDisplay : MonoBehaviour {
	Text text;
	public UDPReceive udpreceive;
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (udpreceive.getLastReceivedRFID() != null)
			text.text = "NFC ID=" + udpreceive.getLastReceivedRFID();
	}}
