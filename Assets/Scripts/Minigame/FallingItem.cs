using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		//gameObject.transform.
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.localPosition.y < -220) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name.Equals ("Basket (Clone)")) {
			GameObject.Find ("MiniGame").GetComponent <Minigame> ().IncrementScore ();
		}
	}
}
