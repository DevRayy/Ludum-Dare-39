using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTextController : MonoBehaviour {

	private TextMesh textMesh;
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		textMesh.text = "TIME\n" + ((int)Time.time);
	}
}
