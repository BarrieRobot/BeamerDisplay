using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hightlighter : MonoBehaviour {

	public GameObject highlight;
	public Vector3 NFCLocation;
	public GameObject SelectButton;
	public GameObject LeftButton;
	public GameObject RightButton;
	public GameObject RightHoleIndication;
	public GameObject LeftHoleIndication;

	public GameObject canvas;

	private bool NFCHighlighenabled = false;
	private GameObject spawnedHighlight;
	// Use this for initialization
	void Start () {
		//spawnedHighlight= Instantiate (highlight);
		//spawnedHighlight.transform.SetParent(canvas.transform, false);
		//spawnedHighlight.transform.localPosition = NFCLocation;
		ResetScene();
	}
	
	// Update is called once per frame
	void Update () {
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			ResetScene ();
			break;
		case State.SELECTING:
			disableNFCHighlight ();
			enableCategoryButtons ();
			break;
		default:
			break;
		}
	}

	public void enableNFCHighlight() {
		if (!NFCHighlighenabled) {
			highlight.SetActive (true);
			NFCHighlighenabled = true;
		}
	}

	public void disableNFCHighlight() {
		if (NFCHighlighenabled) {
			highlight.SetActive(false);
			NFCHighlighenabled = false;
		}
	}

	public void enableNavigationButtons() {
		LeftButton.SetActive (true);
		RightButton.SetActive (true);
		SelectButton.SetActive (true);
	}

	public void enableCategoryButtons() {
		RightHoleIndication.SetActive (true);
		LeftHoleIndication.SetActive (true);
	}

	public void ResetScene() {
		RightHoleIndication.SetActive (false);
		LeftHoleIndication.SetActive (false);
		LeftButton.SetActive (false);
		RightButton.SetActive (false);
		SelectButton.SetActive (false);
		enableNFCHighlight ();
	}
}
