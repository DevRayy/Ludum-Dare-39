using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float baseSpeed;
	public float rotationBaseSpeed;
	private PlayerPowerController powerController;
	private GameController gc;
//speed power factor

	private float oldMousePos = 0;
	// Use this for initialization

	private Rigidbody rb;
	void Start () {
		oldMousePos = Input.GetAxis("Mouse X");
		rb = GetComponent<Rigidbody>();
		powerController = GetComponent<PlayerPowerController>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
    void FixedUpdate()
    {
		if(!gc.gameRunning)
			return;
		Cursor.lockState = CursorLockMode.Locked;
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

 		Vector3 temp = new Vector3(x, 0, z).normalized;
 		Vector3 rotatedVelocity = Quaternion.LookRotation(transform.forward) * temp;
		float speed = baseSpeed + powerController.power * 15 / 100.0f;
		rb.velocity = rotatedVelocity * speed;

		float mousePos = Input.GetAxis("Mouse X");
		float rotationDiff = (mousePos) * rotationBaseSpeed;
		oldMousePos = mousePos;
		transform.Rotate(0, rotationDiff, 0);
		//Cursor.lockState = CursorLockMode.None;
    }
}
