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
	private Vector3 movedir;

	void Update ()
	{
		if (gravitating && (gameObject.transform.localPosition.x > 1 || gameObject.transform.localPosition.x < -1)) {
			// Item is gravitating to the centre
			float pctFromMid = gameObject.transform.localPosition.x / SceneGlobals.sceneBound;
			float newVel = pctFromMid * targetVel;
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.Scale (movedir, new Vector3 (newVel, 0, 0));
			//gameObject.GetComponent<Rigidbody> ().position = new Vector3 (newX, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);

		} else if (gravitating) {
			// Item has gravitated within bounds [-1, 1]
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			gameObject.transform.localPosition = new Vector3 (0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
			gravitating = false;
		}

		if (moveout) {
			// Item needs to move out of the scene
			float newVel = Mathf.Lerp (Mathf.Abs (gameObject.GetComponent<Rigidbody> ().velocity.x), targetVel, Time.deltaTime * moveOutInterpSpeed);
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.Scale (movedir, new Vector3 (newVel, 0, 0));
		}
	}

	public void stopGravity ()
	{
		gravitating = false;
	}

	public void moveOut (Direction dir)
	{
		// Move this object out of the scene, sets the direction and a boolean
		// Actual moving is done in Update 
		movedir = dir == Direction.RIGHT ? Vector3.right : Vector3.left;
		moveout = true;
	}
}
