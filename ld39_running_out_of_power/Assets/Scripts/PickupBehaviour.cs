using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour {

	public int power;
	public float rotationSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter(Collider other) {
         if (other.tag == "Player") {
             other.GetComponent<PlayerPowerController>().addPower(power);
			 Destroy(this.gameObject);
         }
     }
}
