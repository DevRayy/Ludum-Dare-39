using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private float rotationOffset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
		rotationOffset = transform.rotation.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = Vector3.Lerp(transform.position, player.transform.position+offset, 0.95f);
		transform.position = newPos;
		transform.rotation = player.transform.rotation;
		// transform.LookAt(player.transform);
		transform.eulerAngles = new Vector3(rotationOffset, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
