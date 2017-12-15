using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackProvider : MonoBehaviour
{

	public GameObject feedbackObject;
	public UDPReceive udpreceiver;
	public TouchInterpreter touchInterpreter;

	public float sceneBoundMinY = -4.5f;
	public float sceneBoundMaxY = 4.5f;

	public float sceneBoundMinX = -8.5f;
	public float sceneBoundMaxX = 8.5f;

	private List<GameObject> activeFeedback;
	private GameObject Display;
	// Use this for initialization
	void Start ()
	{
		activeFeedback = new List<GameObject> ();
		Display = GameObject.Find ("Display1");
	}
	
	// Update is called once per frame
	void Update ()
	{
		destroyFeedback ();
		List<Vector2> touches = touchInterpreter.getTouchPoints ();
		foreach (Vector2 location in touches) {
			//Debug.Log ("adding feedback at " + location);
			GameObject fb = Instantiate (feedbackObject, new Vector2 (scalePointX (location.x), invert (scalePointY (location.y))), Quaternion.identity);
			fb.transform.SetParent (Display.transform);
			activeFeedback.Add (fb);
			fb.transform.position = new Vector2 (scalePointX (location.x), invert (scalePointY (location.y)));
		}
	}

	private float scalePointX (float point)
	{
		return Mathf.Lerp (sceneBoundMinX, sceneBoundMaxX, point);
	}

	private float scalePointY (float point)
	{
		return Mathf.Lerp (sceneBoundMinY, sceneBoundMaxY, point);
	}

	private float invert (float point)
	{
		return -point;
	}

	private void destroyFeedback ()
	{
		foreach (GameObject fb in activeFeedback) {
			if (fb != null)
				Destroy (fb);
		}
	}
}


