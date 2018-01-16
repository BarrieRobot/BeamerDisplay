using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackProvider : MonoBehaviour
{

	public GameObject feedbackObject;

	public GameObject MinigameBasket;
	public float MinigameTouchHeight;

	public UDPReceive udpreceiver;
	public TouchInterpreter touchInterpreter;

	public float sceneBoundMinY;
	public float sceneBoundMaxY;

	public float sceneBoundMinX;
	public float sceneBoundMaxX;

	private List<GameObject> activeFeedback;
	private GameObject Display;

	public bool debug = false;
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
		if (debug) {
			GameObject fb1 = Instantiate (feedbackObject, new Vector2 (scalePointX (1), invert (scalePointY (1))), Quaternion.identity);
			GameObject fb2 = Instantiate (feedbackObject, new Vector2 (scalePointX (1), invert (scalePointY (0))), Quaternion.identity);
			GameObject fb3 = Instantiate (feedbackObject, new Vector2 (scalePointX (0), invert (scalePointY (0))), Quaternion.identity);
			GameObject fb4 = Instantiate (feedbackObject, new Vector2 (scalePointX (0), invert (scalePointY (1))), Quaternion.identity);
			activeFeedback.Add (fb1);
			activeFeedback.Add (fb2);
			activeFeedback.Add (fb3);
			activeFeedback.Add (fb4);
		}
		List<Vector2> touches = touchInterpreter.getTouchPoints ();
		if (CurrentState.currentState.Equals (State.PREPARING)) {
			List<Vector2> basketLocations = new List <Vector2> ();
			foreach (Vector2 location in touches) {
				if (location.y > MinigameTouchHeight) {
					basketLocations.Add (location);
					//GameObject fb = Instantiate (MinigameBasket, new Vector2 (scalePointX (location.x), invert (scalePointY (location.y))), Quaternion.identity);
					//activeFeedback.Add (fb);
				} else {
					GameObject fb = Instantiate (feedbackObject, new Vector2 (scalePointX (location.x), invert (scalePointY (location.y))), Quaternion.identity);
					activeFeedback.Add (fb);
				}
			}
			if (basketLocations.Count > 0) {
				Vector2 basketLoc = getAverage (basketLocations);
				GameObject basket = Instantiate (MinigameBasket, new Vector2 (scalePointX (basketLoc.x), invert (scalePointY (basketLoc.y))), Quaternion.identity);
				activeFeedback.Add (basket);
			} else {
				Vector2 basketLoc = new Vector2 (0.5f, 0.85f);
				GameObject basket = Instantiate (MinigameBasket, new Vector2 (scalePointX (basketLoc.x), invert (scalePointY (basketLoc.y))), Quaternion.identity);
				activeFeedback.Add (basket);
			}
		} else {
			foreach (Vector2 location in touches) {
				//Debug.Log ("adding feedback at " + location);
				GameObject fb = Instantiate (feedbackObject, new Vector2 (scalePointX (location.x), invert (scalePointY (location.y))), Quaternion.identity);
				//fb.transform.SetParent (Display.transform);
				activeFeedback.Add (fb);
				//fb.transform.position = new Vector2 (scalePointX (location.x), invert (scalePointY (location.y)));
			}
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

	private Vector2 getAverage (List<Vector2> Points)
	{
		float sumX = 0;
		float sumY = 0;
		foreach (Vector2 p in Points) {
			sumX += p.x;
			sumY += p.y;
		}
		Vector2 result = new Vector2 (sumX / Points.Count, sumY / Points.Count);
		return result;
	}
}


