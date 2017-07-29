using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerController : MonoBehaviour {

	[Range(0, 100)]
	public int power;
	public Light spotlight;
	public Light areaLight;
	public Color startColor;
	public Color endColor;
	public Object particleObject;

	void Start () {
		StartCoroutine(DepletePowerOverTime());
	}
	
	// Update is called once per frame
	void Update () {
		spotlight.spotAngle = 30 + power * 60/100.0f;
		Color c = Color.Lerp(endColor, startColor, power/100.0f);
		spotlight.color = c;
		areaLight.color = c;
	}

	public void addPower(int add) {
		Debug.Log("POWER! " + add);
		power += add;
		power = Mathf.Clamp(power, 0, 100);
		if(power == 0)
			Kill();
	}

	private IEnumerator DepletePowerOverTime() {
		while(true) {
			yield return new WaitForSeconds(1.0f);
			addPower(-1);
		}
	}

	private void Kill() {
		// InstantiateParticles();
		// Destroy(this.gameObject);
	}
		
	private void InstantiateParticles() {
		for(int i=-4; i<4; i++) {
			for(int j=-4; j<4; j++) {
				for(int k=-4; k<4; k++) {
					Vector3 pos = new Vector3(transform.position.x + i*0.1f,
											transform.position.y + j*0.1f,
											transform.position.z + k*0.1f);
					GameObject particle = (GameObject) Instantiate(particleObject, pos, transform.rotation);
					particle.GetComponent<Rigidbody>().AddExplosionForce(150, transform.position, 15);
				}
			}
		}
	}
}
