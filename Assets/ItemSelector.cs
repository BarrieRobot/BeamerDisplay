using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour {

	public GameObject[] Sodas;
	public GameObject[] Coffees;

	public GameObject active;

	// Use this for initialization
	void Start () {
		EventManager.OnClicked += Move;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onEnable () {
		EventManager.OnClicked += Move;
	}

	void onDisable () {
		EventManager.OnClicked -= Move;
	}

	void Move(int dir) {
	}
}
