using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using SimpleJSON;
using System.IO;

public class TouchInterpreter : MonoBehaviour
{

	public UDPReceive receiver;

	List<Vector2> touchPoints = new List<Vector2> ();

	private bool RightButtonTouched = false;
	private bool LeftButtonTouched = false;
	private bool SelectButtonTouched = false;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		touchPoints.Clear ();
		if (receiver != null) {
			JSONArray points = receiver.getLastCursors ();
			if (points != null) {
				//Debug.Log ("points there");

				foreach (object p in points) {
					JSONNode point = JSON.Parse (p.ToString ());
					//	Debug.Log (point.AsArray[0].AsFloat + " " + point.AsArray[1].AsFloat);
					Vector2 pointVec = new Vector2 (point.AsArray [0].AsFloat, point.AsArray [1].AsFloat);
					touchPoints.Add (pointVec);
					/*
					if (isTouchInBounds (pointVec, leftButtonBoundsMin, leftButtonBoundsMax)) {
						LeftButtonTouched = true;
					} else if (isTouchInBounds (pointVec, rightButtonBoundsMin, rightButtonBoundsMax)) {
						RightButtonTouched = true;
					} else if (isTouchInBounds (pointVec, selectButtonBoundsMin, selectButtonBoundsMax)) {
						SelectButtonTouched = true;
					} else if (CurrentState.currentState.Equals(State.CHOOSING_CATEGORY) 
						&& (isTouchInBounds(pointVec, HotSelectionMin, HotSelectionMax) ||
						isTouchInBounds(pointVec, ColdSelectionMin, ColdSelectionMax))) {
						stateManager.ChangeState (State.SELECTING);
					} else {
						LeftButtonTouched = false;
						RightButtonTouched = false;
						SelectButtonTouched = false;
					}*/
				}
			}
		}
	}

	public List<Vector2> getTouchPoints ()
	{
		return touchPoints;
	}

	public Vector2 getAverageTouchPoint ()
	{
		float sumX = 0;
		float sumY = 0;
		foreach (Vector2 p in touchPoints) {
			sumX += p.x;
			sumY += p.y;
		}
		Vector2 result = new Vector2 (sumX / touchPoints.Count, sumY / touchPoints.Count);
		Debug.Log ("Average hit is: [" + result.x + ", " + result.y + "]");
		return result;
	}

	bool isTouchInBounds (Vector2 touch, Vector2 minBound, Vector2 maxBound)
	{
		if (touch.x > minBound.x && touch.x < maxBound.x
		    && touch.y > minBound.y && touch.y < maxBound.y) {
			return true;
		}
		return false;
	}

	public Vector2 getScaledAverageTouchPoint ()
	{
		return scalePoint (getAverageTouchPoint ());
	}

	// Scales to [-5 ... 5]
	public Vector2 scalePoint (Vector2 p)
	{
		return new Vector2 ((p.x - 0.5f) * 10, p.y);
	}
}
