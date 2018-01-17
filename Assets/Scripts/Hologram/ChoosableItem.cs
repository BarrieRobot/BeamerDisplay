using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosableItem : MonoBehaviour
{
	
	//	public ItemType type;
	public string name;

	public DrinkType type;
	public Drink drink;

	private float targetVel = 700;
	private float moveOutInterpSpeed = 5;

	private bool gravitating = true;
	private bool moveout = false;
	private Direction movedir;
	private bool falling = false;

	private float price = 0;

	void Start ()
	{
		try {
			price = DatabaseManager.prices [(int)drink];
		} catch (Exception e) {
			price = 0;
		}
	}

	void Update ()
	{
		if (gameObject.transform.localPosition.y < -500)
			Destroy (gameObject);
		if (falling && gameObject.transform.localPosition.y < -10) {
			GetComponent<Rigidbody> ().useGravity = false;//velocity = Vector3.Scale (Vector3.down, moveVelocity * Time.deltaTime);
			GetComponent<Rigidbody> ().isKinematic = true;//velocity = Vector3.Scale (Vector3.down, moveVelocity * Time.deltaTime);
			GetComponentInChildren<Levitate> ().enabled = true;
			falling = false;
		}
		if (gravitating && (gameObject.transform.localPosition.x > 1 || gameObject.transform.localPosition.x < -1)) {
			float pctFromMid = gameObject.transform.localPosition.x / SceneGlobals.sceneBound;
			float newVel = pctFromMid * targetVel;
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.Scale (movedir == Direction.RIGHT ? Vector3.right : Vector3.left, new Vector3 (newVel, 0, 0));
			//gameObject.GetComponent<Rigidbody> ().position = new Vector3 (newX, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

		} else if (gravitating) {
			// Item has gravitated within bounds [-1, 1]
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			gameObject.transform.localPosition = new Vector3 (0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
			gravitating = false;
		}

		if (moveout) {
			float newVel = Mathf.Lerp (Mathf.Abs (gameObject.GetComponent<Rigidbody> ().velocity.x), targetVel, Time.deltaTime * moveOutInterpSpeed);
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.Scale (movedir == Direction.RIGHT ? Vector3.right : Vector3.left, new Vector3 (newVel, 0, 0));
		}
	}

	public void Fall (bool enable)
	{
		if (enable) {
			falling = true;
		} else {
			falling = false;
		}
	}

	public void stopGravity ()
	{
		gravitating = false;
	}

	public void moveOut (Direction dir)
	{
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().useGravity = false;

		movedir = dir;
		moveout = true;
	}

	public float getPrice ()
	{
		return price;
	}
}
