using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{

	public float magnitude;
	public float speed;

	private float startY;
	private bool up = true;

	private float progress = 0;
	// Use this for initialization
	void Start ()
	{
		startY = gameObject.transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		progress += Time.deltaTime;
		float offset = Mathf.Sin (progress * speed) * magnitude;
		gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, startY + offset, gameObject.transform.localPosition.z);
	}
}
