using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name.Equals ("bean(Clone)")) {
			GameObject.Find ("MiniGame").GetComponent <Minigame> ().IncrementScore ();
			Destroy (other.gameObject);
		}
	}

}
