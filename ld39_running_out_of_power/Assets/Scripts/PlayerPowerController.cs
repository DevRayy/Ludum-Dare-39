using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerController : MonoBehaviour {

	[Range(0, 1000)]
	public int power;
	public Light spotlight;
	public Light areaLight;
	public Color startColor;
	public Color endColor;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Color c = Color.Lerp(endColor, startColor, power/1000.0f);
		spotlight.color = c;
		areaLight.color = c;
	}

	public void addPower(int add) {
		Debug.Log("POWER! " + add);
		power += add;
		power = Mathf.Clamp(power, 0, 1000);
		if(power == 0)
			Kill();
	}

	private void Kill() {

	}
}
