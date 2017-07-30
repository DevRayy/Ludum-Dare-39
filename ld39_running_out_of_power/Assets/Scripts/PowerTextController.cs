using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTextController : MonoBehaviour {

	private PlayerPowerController ppc;
	private TextMesh textMesh;
	void Start () {
		ppc = GetComponentInParent<PlayerPowerController>();
		textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		textMesh.text = "POWER\n" + ppc.power;
	}
}
