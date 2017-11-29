﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Right button triggered");
		EventManager.Execute (Direction.RIGHT);
	}
}