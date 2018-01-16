using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{

	int score = 0;
	Text scoreText;

	// Use this for initialization
	void Start ()
	{
		EventManager.OnStartMinigame += ResetScore;
		scoreText = gameObject.GetComponentInChildren <Text> ();
	}

	void OnEnable ()
	{
		EventManager.OnStartMinigame += ResetScore;
	}

	void OnDisable ()
	{
		EventManager.OnStartMinigame -= ResetScore;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void ResetScore ()
	{
		score = 0;
	}

	public void IncrementScore ()
	{
		score += 1;
		scoreText.text = "Minigame score: " + score;
	}
}
