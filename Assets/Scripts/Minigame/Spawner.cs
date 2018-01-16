using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject CoffeeBean;
	public GameObject SugarCube;
	public float SpawnDelayMin;
	public float SpawnDelayMax;

	float nextSpawnDelay;

	bool running = false;
	float timer = 0;


	public float spawnY = 250;
	public float spawnXmin = -200;
	public float spawnXmax = 200;
	// Use this for initialization
	void Start ()
	{
		nextSpawnDelay = Random.Range (SpawnDelayMin, SpawnDelayMax);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (CurrentState.currentState) {
		case State.PREPARING:
			running = true;
			break;
		default: 
			running = false;
			break;
		}
		if (running) {
			timer += Time.deltaTime;
			CurrentState.drink.Equals (DrinkType.COLD);
			if (timer > nextSpawnDelay) {
				timer = 0;
				nextSpawnDelay = Random.Range (SpawnDelayMin, SpawnDelayMax);
				Spawn ();
			}
		}
	}

	void Spawn ()
	{
		if (CurrentState.drink.Equals (DrinkType.COLD)) {
			Instantiate (SugarCube, new Vector3 (Random.Range (spawnXmin, spawnXmax), spawnY, 0), Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360))));
		} else {
			Instantiate (CoffeeBean, new Vector3 (Random.Range (spawnXmin, spawnXmax), spawnY, 0), Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360))));
		}
	}
}
