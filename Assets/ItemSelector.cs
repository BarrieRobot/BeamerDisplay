using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour {

	public GameObject[] Sodas;
	public GameObject[] Coffees;
	public Vector3 moveVelocity;
	public GameObject DrinkNameText;

	public Hashtable itemnames; 
	public Dictionary<string, string> MyDictionary;

	public float LeftBound;
	public float RightBound;

	private GameObject active;
	private bool inAction = false;

	private int current = 0;

	// Use this for initialization
	void Start () {
		EventManager.OnClicked += Move;
		EventManager.OnClickedSelect += Select;
		EventManager.OnDrinkTypeChange += ChangeType;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentState.currentState == State.SELECTING) {
			if (!inAction) {
				InstantiateNewItem (0);
				inAction = true;
			} else {
				if (active.transform.localPosition.x < LeftBound) {
					Destroy (active);
					InstantiateNewItem (-1);
				} else if (active.transform.localPosition.x > RightBound) {
					Destroy (active);
					InstantiateNewItem (1);
				}
			}
		} else {
			inAction = false;
		}
	}

	void InstantiateNewItem (int delta) {
		current += delta;
		switch (CurrentState.drink) {
		case DrinkType.HOT:
			current = current % Coffees.Length;
			active = Instantiate (Coffees [current], transform.parent);
			break;
		case DrinkType.COLD:
			current = current % Sodas.Length;
			active = Instantiate (Sodas [current], transform.parent);
			break;
		default:
			break;
		}
	}

	void onEnable () {
		EventManager.OnClicked += Move;
		EventManager.OnClickedSelect += Select;
		EventManager.OnDrinkTypeChange += ChangeType;
	}

	void onDisable () {
		EventManager.OnClicked -= Move;
		EventManager.OnClickedSelect -= Select;
		EventManager.OnDrinkTypeChange -= ChangeType;
	}

	void Move(Direction dir) {
		if (dir.Equals (Direction.LEFT)) {
			active.GetComponent<Rigidbody> ().velocity = Vector3.Scale(Vector3.left, moveVelocity * Time.deltaTime);
		} else { //right
			active.GetComponent<Rigidbody> ().velocity = Vector3.Scale(Vector3.right, moveVelocity * Time.deltaTime);
		}
	}

	void Select() {
		active.GetComponent<Rigidbody> ().velocity = Vector3.Scale(Vector3.down, moveVelocity * Time.deltaTime);
		CurrentState.currentState = State.CONFIRMING;
		SetDrinkName ();
	}

	void ChangeType() {
		if (active.GetComponent<ChoosableItem> ().type != CurrentState.drink) {
			Destroy (active);
			InstantiateNewItem (0);
		}
	}

	void SetDrinkName() {
		DrinkNameText.GetComponent<Text> ().text = active.GetComponent<ChoosableItem> ().name;
	}
}
