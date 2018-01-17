using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class HoleIndicator : MonoBehaviour
{

	Vector3 desiredScale;
	public float sizeChangeTime;

	bool activating = false;
	bool dactivating = false;
	// Use this for initialization
	void Start ()
	{
		gameObject.transform.localScale = Vector3.zero;
		StartCoroutine ("sizeup");
	}

	void OnEnable ()
	{
		if (!activating) {
			activating = true;
			gameObject.transform.localScale = Vector3.zero;
			StartCoroutine ("sizeup");
		}
	}

	float timer = 0;

	IEnumerator sizeup ()
	{
		timer = 0;
		while (timer < sizeChangeTime) {
			yield return null;
			timer += Time.deltaTime;
			float scale = Mathf.Lerp (0, desiredScale.x, timer / sizeChangeTime);
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
		}
		activating = false;
	}

	IEnumerator sizedown ()
	{
		timer = sizeChangeTime;
		while (timer < sizeChangeTime) {
			yield return null;
			timer -= Time.deltaTime;
			float scale = Mathf.Lerp (0, desiredScale.x, timer / sizeChangeTime);
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
		}
		dactivating = false;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Deactivate ()
	{
		if (!dactivating) {
			StartCoroutine ("sizedown");
		}
	}
}
