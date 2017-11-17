using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hightlighter : MonoBehaviour {

	public GameObject highlight;
	public Vector2 NFCLocation;
	public GameObject SelectButton;
	public GameObject LeftButton;
	public GameObject RightButton;

	private bool NFCHighlighenabled = false;
	private GameObject spawnedHighlight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			enableNFCHighlight ();
			break;
		case State.SELECTING:
			disableNFCHighlight ();
			break;
		default:
			break;
		}
	}

	public void enableNFCHighlight() {
		if (!NFCHighlighenabled) {
			spawnedHighlight= Instantiate (highlight, NFCLocation, Quaternion.identity);
			NFCHighlighenabled = true;
		}
	}

	public void disableNFCHighlight() {
		if (NFCHighlighenabled) {
			Destroy (spawnedHighlight);
			NFCHighlighenabled = false;
		}
	}
}
