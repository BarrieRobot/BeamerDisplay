using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	
	public float WorldRotationSpeed = 1;
	public float SelfRotationSpeed = 1;
	public bool x, y, z;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.Rotate (new Vector3 (x ? WorldRotationSpeed * Time.deltaTime : 0
			, y ? WorldRotationSpeed * Time.deltaTime : 0
			, z ? WorldRotationSpeed * Time.deltaTime : 0), Space.World);
		gameObject.transform.Rotate (new Vector3 (x ? SelfRotationSpeed * Time.deltaTime : 0
			, y ? SelfRotationSpeed * Time.deltaTime : 0
			, z ? SelfRotationSpeed * Time.deltaTime : 0), Space.Self);
	}
}
