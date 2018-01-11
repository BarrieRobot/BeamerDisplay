using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowParameters : MonoBehaviour
{

	[Range (0, 2)]
	public float speed;

	public float xradius;
	public float zradius;

	public float exitDuration;
	public float interval;

	private float xradreduce;
	private float zradreduce;
	private int steps;
	private float startspeed;

	private float startxradius;
	private float startzradius;

	void Start ()
	{
		EventManager.OnCategoryChosen += ExitShow;
		//i = interval * 
		steps = (int)(exitDuration / interval);
		xradreduce = xradius * (1f / steps) * 0.5f;
		zradreduce = zradius * (1f / steps) * 0.5f;
	}

	void OnEnable ()
	{
		EventManager.OnCategoryChosen += ExitShow;
		xradius = 300;
		zradius = 250;
		speed = 0.53f;
	}

	void OnDisable ()
	{
		EventManager.OnCategoryChosen -= ExitShow;
	}

	public void ExitShow ()
	{
		speed = speed * 1.5f;
		StartCoroutine ("speedUp");	
	}

	IEnumerator speedUp ()
	{
		for (int i = 0; i < steps; i++) {
			speed *= 1.08f;
			xradius -= (xradreduce);
			zradius -= (zradreduce);
			yield return new WaitForSeconds (interval);
		}
		gameObject.SetActive (false);
	}
}
