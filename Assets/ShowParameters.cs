﻿using System.Collections;
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

	private bool inanimation = false;

	void Start ()
	{
		EventManager.OnDrinkTypeChange += ExitShow;
		//i = interval * 
		steps = (int)(exitDuration / interval);
		xradreduce = xradius * (1f / steps) * 0.5f;
		zradreduce = zradius * (1f / steps) * 0.5f;
	}

	void OnEnable ()
	{
		EventManager.OnDrinkTypeChange += ExitShow;
		xradius = 350;
		zradius = 300;
		speed = 0.53f;
	}

	void OnDisable ()
	{
		EventManager.OnDrinkTypeChange -= ExitShow;
	}

	public void ExitShow ()
	{
		if (isActiveAndEnabled && !inanimation) {
			speed = speed * 1.5f;
			StartCoroutine ("speedUp");	
		}
	}

	IEnumerator speedUp ()
	{
		inanimation = true;
		for (int i = 0; i < steps + 20; i++) {
			speed *= 1.1f;
			xradius -= (xradreduce);
			zradius -= (zradreduce);
			yield return new WaitForSeconds (interval);
		}
		EventManager.ExecuteShowOver ();
		inanimation = false;
		gameObject.SetActive (false);
	}
}
