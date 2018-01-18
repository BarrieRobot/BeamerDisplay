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
	public Animator RightHoleanim;
	public Animator LeftHoleanim;
	public GameObject LeftHoleIndication;
	public GameObject ScanTagInfo;
	public GameObject SelectTypeInfo;
	public GameObject SelectedItemName;
	public GameObject ScanToVerify;
	//	public GameObject BeingPrepared;
	public GameObject CancelChoice;
	public GameObject NewOrder;
	public GameObject MinigameScore;
	public GameObject OrderCompleteText;

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

	private float completetimer = 0;

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
			enableBothIndicators (false);
			//activateSimpleHoleIndicators ();
			break;
		case State.CHOOSING_CATEGORY:
			DisableAll ();
			completetimer = 0;
			enableCategoryButtons ();
			displayIncompleteOrders ();
			enableBothIndicators (true);
			break;
		case State.SELECTING:
			DisableAll ();
			enableNavigation ();
			activateCorrectIndicator ();
			break;
		case State.CONFIRMING:
			DisableAll ();
			enableNFCHighlight ();
			enableBothIndicators (false);
			enableConfirmNFC ();
			break;
		case State.PREPARING:
			DisableAll ();
			enableBothIndicators (false);
			enablePreparingDrink ();
			break;
		case State.COMPLETED:
			DisableAll ();
			incrementCompleteTimer ();
			enableCompletedText ();
			activateCorrectIndicator ();
			break;
		default:
			break;
		}
	}

	public void incrementCompleteTimer ()
	{
		completetimer += Time.deltaTime;
		if (completetimer > 10) {
			completetimer = 0;
			CurrentState.currentState = State.WAIT_FOR_NFC;
		}
	}

	public void displayIncompleteOrders ()
	{
		Text[] texts = FoundOrdersList.GetComponentsInChildren <Text> ();
		ExistingOrder[] orders = FoundOrdersList.GetComponentsInChildren <ExistingOrder> ();
		if (DatabaseManager.ordersFound) {
			SelectTypeInfo.SetActive (false);
			OrdersFoundText.SetActive (true);
			FoundOrdersList.SetActive (true);
			string text = "";
			int i = 0;
			foreach (Order order in DatabaseManager.lastReceivedOrders) {
				if (texts.Length > i) {
					texts [i].text = "" + order.item;//+ " om\n" + order.orderTime;
					texts [i].gameObject.SetActive (true);
					orders [i].drinkid = order.itemid;
					orders [i].setDrinkType (order.drinkType);
					orders [i].setDrinkName (order.item);
				}
				i++;
			}
			for (; i < texts.Length; i++) {
				texts [i].gameObject.SetActive (false);
			}
		} else {
			foreach (Text t in texts) {
				t.text = "";
			}
		}
	}

	public void enableCompletedText ()
	{
		NewOrder.SetActive (true);
		OrderCompleteText.SetActive (true);
	}

	public void enableNFCHighlight ()
	{
		highlight.SetActive (true);
		NFCHighlighenabled = true;
	}

	public void enableConfirmNFC ()
	{
		SelectedItemName.SetActive (true);
		ScanTagInfo.SetActive (true);
		//ScanToVerify.SetActive (true);
		CancelChoice.SetActive (true);
	}

	public void enablePreparingDrink ()
	{
		//BeingPrepared.SetActive (true);
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
			RightHoleanim.SetBool ("big", true);
			RightHoleanim.SetBool ("small", false);

			LeftHoleanim.SetBool ("big", false);
			LeftHoleanim.SetBool ("small", true);

			HotDrinkIndicatorActive.SetActive (true);
			ColdDrinkIndicatorInactive.SetActive (true);
		} else if (CurrentState.drink.Equals (DrinkType.COLD)) {
			RightHoleanim.SetBool ("big", false);
			RightHoleanim.SetBool ("small", true);
			
			LeftHoleanim.SetBool ("big", true);
			LeftHoleanim.SetBool ("small", false);

			HotDrinkIndicatorInactive.SetActive (true);
			ColdDrinkIndicatorActive.SetActive (true);
		} else {
			Debug.LogError ("No active drink?");
		}
	}

	void enableBothIndicators (bool enabler)
	{
		RightHoleanim.SetBool ("big", enabler);
		RightHoleanim.SetBool ("small", !enabler);

		LeftHoleanim.SetBool ("big", enabler);
		LeftHoleanim.SetBool ("small", !enabler);

	}

	public void enableNavigation ()
	{
		LeftButton.SetActive (true);
		RightButton.SetActive (true);
		SelectButton.SetActive (true);
	}

	public void enableCategoryButtons ()
	{
		//RightHoleIndication.SetActive (false);
		//LeftHoleIndication.SetActive (false);
		ScanTagInfo.SetActive (false);
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
		//RightHoleIndication.GetComponent<HoleIndicator> ().Deactivate ();
		//LeftHoleIndication.GetComponent<HoleIndicator> ().Deactivate ();
		OrderCompleteText.SetActive (false);
		LeftButton.SetActive (false);
		RightButton.SetActive (false);
		SelectButton.SetActive (false);
		ScanTagInfo.SetActive (false);
		highlight.SetActive (false);
		SelectedItemName.SetActive (false);
		//ScanToVerify.SetActive (false);
		//BeingPrepared.SetActive (false);
		CancelChoice.SetActive (false);
		NewOrder.SetActive (false);
		MinigameScore.SetActive (false);

		OrdersFoundText.SetActive (false);
		FoundOrdersList.SetActive (false);
	}
}
