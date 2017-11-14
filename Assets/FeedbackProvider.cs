using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackProvider : MonoBehaviour {

	public GameObject feedback;
	public UDPReceive udpreceiver;

	private GameObject activeFeedback;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {
			if (activeFeedback != null) Destroy(activeFeedback);
			
			Vector3 pos = Input.mousePosition;
			Debug.Log (pos);
			pos = Camera.main.ScreenToWorldPoint (pos);
			pos.z = 0;
			activeFeedback = Instantiate (feedback, pos, Quaternion.identity);
		}
	}

}
