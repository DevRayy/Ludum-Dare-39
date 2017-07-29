using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float baseSpeed;
	public float rotationBaseSpeed;
	private PlayerPowerController powerController;
//speed power factor

	private float oldMousePos = 0;
	// Use this for initialization

	private Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
		powerController = GetComponent<PlayerPowerController>();
	}
	
    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

 		Vector3 temp = new Vector3(x, 0, z).normalized;
 		Vector3 rotatedVelocity = Quaternion.LookRotation(transform.forward) * temp;
		 float speed = baseSpeed + powerController.power * 15 / 1000.0f;
		rb.velocity = rotatedVelocity * speed;

		float mousePos = Input.mousePosition.x;
		float rotationDiff = (mousePos - oldMousePos) * Time.deltaTime * rotationBaseSpeed;
		oldMousePos = mousePos;
		transform.Rotate(0, rotationDiff, 0);
    }
}
