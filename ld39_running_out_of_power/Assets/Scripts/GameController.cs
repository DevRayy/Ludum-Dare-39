using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] enableOnStart;
	public GameObject[] disableOnStart;
	public bool gameRunning = false;
	public bool gameEnded = false;
	public float gameStartTime = 0.0f;
	void Start () {
		foreach (GameObject g in enableOnStart){
			g.active = false;
		}
		foreach (GameObject g in disableOnStart){
			g.active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameRunning && !gameEnded) {
			if(Input.GetKeyDown(KeyCode.Space))
				EnableGame(true);
		}

		if(Input.GetKeyDown(KeyCode.R))
			RestartGame();
	}

	public void EndGame(int score) {
		EnableGame(false);
		gameEnded = true;
		disableOnStart[1].GetComponent<TextMesh>().text = "You run out of power\nYou lasted " + score + " seconds\n\nPress R to restart";
	}

	void EnableGame(bool enable) {
		if(enable)
			gameStartTime = Time.time;
		gameRunning = enable;
		foreach (GameObject g in enableOnStart){
			g.active = enable;
		}
		foreach (GameObject g in disableOnStart){
			g.active = !enable;
		}
	}

	public void RestartGame() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
