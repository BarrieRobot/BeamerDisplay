using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using SimpleJSON;

public class TouchInterpreter : MonoBehaviour {

	public UDPReceive receiver;
	List<Vector2> touchPoints = new List<Vector2>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (receiver != null) {
			touchPoints.Clear ();
			JSONArray points = receiver.getLastCursors ();
			if (points != null) {
				foreach (object p in points) {
					JSONNode point = JSON.Parse (p.ToString ());
					//	Debug.Log (point.AsArray[0].AsFloat + " " + point.AsArray[1].AsFloat);
					touchPoints.Add (new Vector2 (point.AsArray [0].AsFloat, point.AsArray [1].AsFloat));
				}
			}
		}
	}

	public List<Vector2> getTouchPoints() {
		return touchPoints;
	}

	public Vector2 getAverageTouchPoint() {
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

	public Vector2 getScaledAverageTouchPoint () {
		return scalePoint (getAverageTouchPoint ());
	}

	// Scales to [-5 ... 5]
	public Vector2 scalePoint(Vector2 p) {
		return new Vector2 ((p.x - 0.5f) * 10, p.y);
	}
}
