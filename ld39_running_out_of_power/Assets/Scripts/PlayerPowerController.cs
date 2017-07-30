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
	public AudioClip audioClip;
	public GameObject audioPlayer;

	private GameController gc;
	private bool crRunning = false;

	Camera camera;
	float duration = 1.2f;
	float magnitude = 0.8f;  

	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!gc.gameRunning)
			return;
		if(!crRunning)
			StartCoroutine(DepletePowerOverTime());
		spotlight.spotAngle = 30 + power * 60/100.0f;
		Color c = Color.Lerp(endColor, startColor, power/100.0f);
		spotlight.color = c;
		areaLight.color = c;
	}

	public void addPower(int add) {
		power += add;
		power = Mathf.Clamp(power, 0, 100);
		if(power == 0)
			Kill();
	}

	private IEnumerator DepletePowerOverTime() {
		while(true) {
			crRunning = true;
			yield return new WaitForSeconds(1.0f);
			addPower(-1);
		}
	}

	private void Kill() {
		if(!gc.gameRunning)
			return;
		InstantiateParticles();
		StartCoroutine(Shake());
		AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
		audio.clip = audioClip;
		audio.Play();
		// Destroy(this.gameObject);
		GetComponent<MeshRenderer>().enabled = false;
		gc.EndGame(GetComponentInChildren<TimeTextController>().score);
	}
		
	private void InstantiateParticles() {
		for(int i=-3; i<3; i++) {
			for(int j=-3; j<3; j++) {
				for(int k=-3; k<3; k++) {
					Vector3 pos = new Vector3(transform.position.x + i*0.1f,
											transform.position.y + j*0.1f,
											transform.position.z + k*0.1f);
					GameObject particle = (GameObject) Instantiate(particleObject, pos, transform.rotation);
					particle.GetComponent<Rigidbody>().AddExplosionForce(150, transform.position, 15);
				}
			}
		}
	}

	IEnumerator Shake() {  
        float elapsed = 0.0f;
        
        Vector3 originalCamPos = camera.transform.localPosition;
        
        while (elapsed < duration) {
            
            elapsed += Time.deltaTime;          
            
            float percentComplete = elapsed / duration;         
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            
            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            float z = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;
            z *= magnitude * damper;
            
            camera.transform.localPosition = new Vector3(x, y, z) + camera.transform.localPosition;
                
            yield return null;
        }
        
        camera.transform.localPosition = originalCamPos;
    }
}
