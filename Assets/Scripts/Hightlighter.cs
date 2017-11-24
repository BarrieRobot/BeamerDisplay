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
	public GameObject Arrow;
	public GameObject ScanTagInfo;
	public GameObject SelectTypeInfo;
	public GameObject ChooseDrinkInfo;

	public GameObject HotDrinkIndicatorActive;
	public GameObject ColdDrinkIndicatorActive;

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
		case State.CHOOSING_CATEGORY:
			disableNFCHighlight ();
			enableCategoryButtons ();
			break;
		case State.SELECTING:
			disableNFCHighlight ();
			enableNavigation ();
			break;
		default:
			break;
		}
	}

	public void enableNFCHighlight() {
		if (!NFCHighlighenabled) {
			highlight.SetActive (true);
			RightHoleIndication.SetActive (true);
			Arrow.SetActive(true);
			LeftHoleIndication.SetActive (true);
			NFCHighlighenabled = true;
		}
	}

	public void disableNFCHighlight() {
		if (NFCHighlighenabled) {
			Arrow.SetActive(false);
			highlight.SetActive(false);
			RightHoleIndication.SetActive (false);
			LeftHoleIndication.SetActive (false);
			NFCHighlighenabled = false;
		}
	}

	public void enableNavigation() {
		LeftButton.SetActive (true);
		RightButton.SetActive (true);
		SelectButton.SetActive (true);
		ChooseDrinkInfo.SetActive (false);
	}

	public void enableCategoryButtons() {
		RightHoleIndication.SetActive (false);
		LeftHoleIndication.SetActive (false);
		ScanTagInfo.SetActive (false);

		ColdDrinkIndicatorActive.SetActive (true);
		HotDrinkIndicatorActive.SetActive (true);
		SelectTypeInfo.SetActive (true);
	}

	public void ResetScene() {
		ColdDrinkIndicatorActive.SetActive (true);
		HotDrinkIndicatorActive.SetActive (false);
		SelectTypeInfo.SetActive (false);
		RightHoleIndication.SetActive (true);
		LeftHoleIndication.SetActive (true);
		LeftButton.SetActive (false);
		RightButton.SetActive (false);
		SelectButton.SetActive (false);
		ChooseDrinkInfo.SetActive (false);
		enableNFCHighlight ();
	}
}
