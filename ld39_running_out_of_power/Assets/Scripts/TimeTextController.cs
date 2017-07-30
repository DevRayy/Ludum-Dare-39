using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTextController : MonoBehaviour {

	public int score = 0;
	private TextMesh textMesh;
	private GameController gc;
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		score = ((int)(Time.time-gc.gameStartTime));
		// textMesh.text = "TIME\n" + score;
		textMesh.text = "";
	}
}
