using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
	public UDPSend udpsender;
	public GameObject[] Sodas;
	public GameObject[] Coffees;
	public Vector3 moveVelocity;
	public GameObject DrinkNameText;
	public float startDelay;
	public Text namePriceDisplay;

	public float LeftBound;
	public float RightBound;

	private GameObject active;
	private bool inAction = false;

	private int current = 0;

	// Use this for initialization
	void Start ()
	{
		EventManager.OnClicked += Move;
		EventManager.OnClickedSelect += Select;
		EventManager.OnDrinkTypeChange += ChangeType;
		EventManager.OnNFCScanned += FallDown;
		EventManager.OnShowOver += EnableActive;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (CurrentState.currentState == State.SELECTING) {
			if (!inAction) {
				if (active != null)
					Destroy (active);
				
				InstantiateNewItem (0, 0);
				active.SetActive (false);
				inAction = true;
			} else {
				if (active != null) {
					if (active.transform.localPosition.x < LeftBound) {
						Destroy (active);
						InstantiateNewItem (-1, RightBound);
						if (udpsender != null)
							udpsender.requestStock (current);
					} else if (active.transform.localPosition.x > RightBound) {
						Destroy (active);
						InstantiateNewItem (1, LeftBound);
						if (udpsender != null)
							udpsender.requestStock (current);
					}
				}
			}
		} else {
			inAction = false;
			if (CurrentState.currentState == State.WAIT_FOR_NFC ||
			    CurrentState.currentState == State.CHOOSING_CATEGORY
			    && active != null) {
				Destroy (active);
			}
		}
		SetNameAndPriceDisplay ();
	}

	void InstantiateNewItem (int delta, float xPos)
	{
		current += delta;
		switch (CurrentState.drink) {
		case DrinkType.HOT:
			current = current % Coffees.Length;
			if (current < 0)
				current = Coffees.Length - 1;
			active = Instantiate (Coffees [current], transform.parent);
			active.transform.localPosition = new Vector3 (xPos, active.transform.localPosition.y, active.transform.localPosition.z);
			break;
		case DrinkType.COLD:
			current = current % Sodas.Length;
			if (current < 0)
				current = Sodas.Length - 1;
			active = Instantiate (Sodas [current], transform.parent);
			active.transform.localPosition = new Vector3 (xPos, active.transform.localPosition.y, active.transform.localPosition.z);
			break;
		default:
			break;
		}
	}

	void onEnable ()
	{
		EventManager.OnClicked += Move;
		EventManager.OnClickedSelect += Select;
		EventManager.OnDrinkTypeChange += ChangeType;
		EventManager.OnNFCScanned += FallDown;
		EventManager.OnShowOver += EnableActive;
	}

	void onDisable ()
	{
		EventManager.OnClicked -= Move;
		EventManager.OnClickedSelect -= Select;
		EventManager.OnDrinkTypeChange -= ChangeType;
		EventManager.OnNFCScanned -= FallDown;
		EventManager.OnShowOver -= EnableActive;
	}

	void Move (Direction dir)
	{
		active.GetComponent <ChoosableItem> ().stopGravity ();
		active.GetComponent<ChoosableItem> ().moveOut (dir);
	}

	public void FallDown (int i)
	{
		if (CurrentState.currentState == State.PREPARING) {
			active.GetComponent<Rigidbody> ().useGravity = true;//velocity = Vector3.Scale (Vector3.down, moveVelocity * Time.deltaTime);
			active.GetComponent<Levitate> ().enabled = false;
		}
	}

	void Select ()
	{
		//active.GetComponent<Rigidbody> ().AddForce (0, -50, 0);
		CurrentState.currentState = State.CONFIRMING;
		SetDrinkName ();
	}

	void EnableActive ()
	{
		active.SetActive (true);
	}

	void ChangeType ()
	{
		if (active != null && active.GetComponent<ChoosableItem> ().type != CurrentState.drink) {
			current = 0;
			active.GetComponent<Levitate> ().enabled = false;
			active.GetComponent<Rigidbody> ().useGravity = true;//velocity = Vector3.Scale (Vector3.down, moveVelocity * Time.deltaTime);
			active.GetComponent<ChoosableItem> ().Fall (false);
			if (CurrentState.drink == DrinkType.COLD) {
				active = Instantiate (Sodas [current], transform.parent);
			} else {
				active = Instantiate (Coffees [current], transform.parent);
			}
			active.transform.localPosition = new Vector3 (0, 500, active.transform.localPosition.z);
			active.GetComponent<Levitate> ().enabled = false;
			active.GetComponent<Rigidbody> ().useGravity = true;//velocity = Vector3.Scale (Vector3.down, moveVelocity * Time.deltaTime);
			active.GetComponent<ChoosableItem> ().Fall (true);//Destroy (active);
			//InstantiateNewItem (0, 0);
		}
	}

	void SetNameAndPriceDisplay ()
	{
		if (active != null) {
			float price = active.GetComponent<ChoosableItem> ().getPrice ();
			if (price != 0)
				namePriceDisplay.text = active.GetComponent<ChoosableItem> ().name + "\n\u20AC" + price;
		} else {
			namePriceDisplay.text = "";
		}
	}

	void SetDrinkName ()
	{
		if (active != null)
			DrinkNameText.GetComponent<Text> ().text = active.GetComponent<ChoosableItem> ().name;
	}

	public ChoosableItem getCurrentItem ()
	{
		return active != null ? active.GetComponent<ChoosableItem> () : null;
	}
}
