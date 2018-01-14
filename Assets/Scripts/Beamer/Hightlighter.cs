using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hightlighter : MonoBehaviour
{

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
	public GameObject YouSelectedText;
	public GameObject SelectedItemName;
	public GameObject ScanToVerify;
	public GameObject BeingPrepared;
	public GameObject CancelChoice;
	public GameObject NewOrder;
	public GameObject MinigameScore;

	public GameObject OrdersFoundText;
	public GameObject FoundOrdersList;

	public GameObject HotDrinkIndicatorActive;
	public GameObject ColdDrinkIndicatorActive;
	public GameObject HotDrinkIndicatorInactive;
	public GameObject ColdDrinkIndicatorInactive;
	public GameObject canvas;

	private bool NFCHighlighenabled = false;
	private GameObject spawnedHighlight;
	// Use this for initialization

	void Start ()
	{
		//spawnedHighlight= Instantiate (highlight);
		//spawnedHighlight.transform.SetParent(canvas.transform, false);
		//spawnedHighlight.transform.localPosition = NFCLocation;
		DisableAll ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (CurrentState.currentState) {
		case State.WAIT_FOR_NFC:
			DisableAll ();
			enableNFCHighlight ();
			ScanTagInfo.SetActive (true);
			//activateSimpleHoleIndicators ();
			break;
		case State.CHOOSING_CATEGORY:
			DisableAll ();
			enableCategoryButtons ();
			displayIncompleteOrders ();
			break;
		case State.SELECTING:
			DisableAll ();
			enableNavigation ();
			activateCorrectIndicator ();
			break;
		case State.CONFIRMING:
			DisableAll ();
			enableNFCHighlight ();
			enableConfirmNFC ();
			break;
		case State.PREPARING:
			DisableAll ();
			enablePreparingDrink ();
			break;
		default:
			break;
		}
	}

	public void displayIncompleteOrders ()
	{
		if (DatabaseManager.ordersFound) {
			SelectTypeInfo.SetActive (false);
			OrdersFoundText.SetActive (true);
			FoundOrdersList.SetActive (true);
			string text = "";
			foreach (Order order in DatabaseManager.lastReceivedOrders) {
				text += order.item + " om " + order.orderTime + "\n";
			}
			FoundOrdersList.GetComponent<Text> ().text = text;
		}
	}

	public void enableNFCHighlight ()
	{
		highlight.SetActive (true);
		Arrow.SetActive (true);
		NFCHighlighenabled = true;
	}

	public void enableConfirmNFC ()
	{
		YouSelectedText.SetActive (true);
		SelectedItemName.SetActive (true);
		ScanToVerify.SetActive (true);
		CancelChoice.SetActive (true);
	}

	public void enablePreparingDrink ()
	{
		BeingPrepared.SetActive (true);
		NewOrder.SetActive (true);
		MinigameScore.SetActive (true);
	}

	/*public void disableNFCHighlight() {
		Arrow.SetActive(false);
		highlight.SetActive(false);
		ScanTagInfo.SetActive (false);
		RightHoleIndication.SetActive (false);
		LeftHoleIndication.SetActive (false);
	}*/

	public void disableCategorySelection ()
	{
		SelectTypeInfo.SetActive (false);
	}

	public void activateSimpleHoleIndicators ()
	{
		RightHoleIndication.SetActive (true);
		LeftHoleIndication.SetActive (true);
	}

	public void activateCorrectIndicator ()
	{
		if (CurrentState.drink.Equals (DrinkType.HOT)) {
			ColdDrinkIndicatorActive.SetActive (false);
			ColdDrinkIndicatorInactive.SetActive (true);
			HotDrinkIndicatorActive.SetActive (true);
			HotDrinkIndicatorInactive.SetActive (false);
		} else if (CurrentState.drink.Equals (DrinkType.COLD)) {
			ColdDrinkIndicatorActive.SetActive (true);
			ColdDrinkIndicatorInactive.SetActive (false);
			HotDrinkIndicatorActive.SetActive (false);
			HotDrinkIndicatorInactive.SetActive (true);
		} else {
			Debug.LogError ("No active drink?");
		}
	}

	public void enableNavigation ()
	{
		LeftButton.SetActive (true);
		RightButton.SetActive (true);
		SelectButton.SetActive (true);
		ChooseDrinkInfo.SetActive (true);
	}

	public void enableCategoryButtons ()
	{
		/*RightHoleIndication.SetActive (false);
		LeftHoleIndication.SetActive (false);
		ScanTagInfo.SetActive (false);
*/
		ColdDrinkIndicatorActive.SetActive (true);
		HotDrinkIndicatorActive.SetActive (true);
		SelectTypeInfo.SetActive (true);
	}

	public void DisableAll ()
	{
		ColdDrinkIndicatorActive.SetActive (false);
		HotDrinkIndicatorActive.SetActive (false);
		HotDrinkIndicatorInactive.SetActive (false);
		ColdDrinkIndicatorInactive.SetActive (false);
		SelectTypeInfo.SetActive (false);
		RightHoleIndication.SetActive (false);
		LeftHoleIndication.SetActive (false);
		LeftButton.SetActive (false);
		RightButton.SetActive (false);
		SelectButton.SetActive (false);
		ChooseDrinkInfo.SetActive (false);
		ScanTagInfo.SetActive (false);
		Arrow.SetActive (false);
		highlight.SetActive (false);
		YouSelectedText.SetActive (false);
		SelectedItemName.SetActive (false);
		ScanToVerify.SetActive (false);
		BeingPrepared.SetActive (false);
		CancelChoice.SetActive (false);
		NewOrder.SetActive (false);
		MinigameScore.SetActive (false);

		OrdersFoundText.SetActive (false);
		FoundOrdersList.SetActive (false);
	}
}
