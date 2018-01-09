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
}
