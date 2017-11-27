using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	
	public float RotationSpeed = 1;
	public bool x, y, z;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(new Vector3(x ? RotationSpeed * Time.deltaTime : 0
												, y ? RotationSpeed * Time.deltaTime : 0
												, z ? RotationSpeed * Time.deltaTime : 0));
	}
}
