using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItem : MonoBehaviour
{

	public GameObject centre;
	public int startPositionPercentage;
	public ShowParameters parameters;

	float timer = 0;
	float angle = 0;

	private Vector3 startPos;


	// Use this for initialization
	void Start ()
	{
		timer = startPositionPercentage / 100f * Mathf.PI * 2;
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime * parameters.speed;
		this.transform.localPosition =
			new Vector3 ((centre.transform.localPosition.x + Mathf.Sin (timer) * parameters.xradius), 
			startPos.y, 
			((centre.transform.localPosition.z + Mathf.Cos (timer) * parameters.zradius)));
	}
}
