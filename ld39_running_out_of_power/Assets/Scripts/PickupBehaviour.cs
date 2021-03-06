﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour {

	public int power;
	public float rotationSpeed;
	public AudioClip audioClip;
	public GameObject audioPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f * Mathf.Sin(Time.time * 2), transform.position.z);
	}

	void OnTriggerEnter(Collider other) {
         if (other.tag == "Player") {
             other.GetComponent<PlayerPowerController>().addPower(power);
			 AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
			 audio.clip = audioClip;
			 audio.Play();
			 Destroy(this.gameObject);
         }
     }
}
