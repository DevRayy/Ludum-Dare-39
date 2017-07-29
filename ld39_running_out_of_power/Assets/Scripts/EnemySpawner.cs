using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Object enemy;
	public float spawnRate;
	private float nextSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) 
        {
            nextSpawn = Time.time + spawnRate;
			Instantiate(enemy, transform.position, transform.rotation);
		}
	}
}
