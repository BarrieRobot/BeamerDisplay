using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using SimpleJSON;
using System.IO;

public class TouchInterpreter : MonoBehaviour {

	public UDPReceive receiver;
	List<Vector2> touchPoints = new List<Vector2>();
	public string configPath = "/home/jonathan/git/BarrieBeamerDisplay/Assets/Resources/test.txt";

	public Vector2 leftButtonBoundsMin;
	public Vector2 leftButtonBoundsMax;

	public Vector2 rightButtonBoundsMin;
	public Vector2 rightButtonBoundsMax;

	public Vector2 selectButtonBoundsMin;
	public Vector2 selectButtonBoundsMax;

	// Use this for initialization
	void Start () {

		//Read the text from directly from the test.txt file
		ReadConfig();
	}

	void ReadConfig() {
		StreamReader reader = new StreamReader(configPath); 
		string line;
		while ((line = reader.ReadLine()) != null) {
			string[] items = line.Split('=');
			string[] points = items [1].Split (':');
			string[] min = points [0].Split (',');
			string[] max = points [1].Split (',');
			switch (items [0]) {
			case "LeftButton":
				leftButtonBoundsMin = new Vector2 (float.Parse (min [0]), float.Parse (min [1]));
				leftButtonBoundsMax = new Vector2 (float.Parse (max [0]), float.Parse (max [1]));
				break;
			case "RightButton":
				rightButtonBoundsMin = new Vector2 (float.Parse (min [0]), float.Parse (min [1]));
				rightButtonBoundsMax = new Vector2 (float.Parse (max [0]), float.Parse (max [1]));
				break;
			case "SelectButton":
				selectButtonBoundsMin = new Vector2 (float.Parse (min [0]), float.Parse (min [1]));
				selectButtonBoundsMax = new Vector2 (float.Parse (max [0]), float.Parse (max [1]));
				break;
			default:
				break;
			}

		}
	}

	// Update is called once per frame
	void Update () {
		touchPoints.Clear ();
		if (receiver != null) {
			JSONArray points = receiver.getLastCursors ();
			if (points != null) {
				Debug.Log ("points there");
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
