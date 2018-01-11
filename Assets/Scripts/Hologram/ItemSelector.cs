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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (CurrentState.currentState == State.SELECTING) {
			if (!inAction) {
				if (active != null)
					Destroy (active);
				
				StartCoroutine ("StartDelayed");
				inAction = true;
			} else {
				if (active != null)
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
		} else {
			inAction = false;
			if (CurrentState.currentState == State.WAIT_FOR_NFC ||
			    CurrentState.currentState == State.CHOOSING_CATEGORY
			    && active != null) {
				Destroy (active);
			}
		}
	}

	IEnumerator StartDelayed ()
	{
		InstantiateNewItem (0, 0);
		active.SetActive (false);
		yield return new WaitForSeconds (startDelay);
		active.SetActive (true);
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
	}

	void onDisable ()
	{
		EventManager.OnClicked -= Move;
		EventManager.OnClickedSelect -= Select;
		EventManager.OnDrinkTypeChange -= ChangeType;
		EventManager.OnNFCScanned -= FallDown;
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

	void ChangeType ()
	{
		if (active != null && active.GetComponent<ChoosableItem> ().type != CurrentState.drink) {
			Destroy (active);
			InstantiateNewItem (0, 0);
		}
	}

	void SetDrinkName ()
	{
		DrinkNameText.GetComponent<Text> ().text = active.GetComponent<ChoosableItem> ().name;
	}

	public ChoosableItem getCurrentItem ()
	{
		return active != null ? active.GetComponent<ChoosableItem> () : null;
	}
}
