using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoote : MonoBehaviour {
	public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50000f;                                  // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
	public Light gunLight;

	public AudioClip audioClip;
	public GameObject audioPlayer;
                                            
    //private AudioSource gunAudio;                                       // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
	private PlayerPowerController ppc;
    private GameController gc;
    private Camera camera;


        float duration;
        float magnitude = 0.05f;  


    void Start () 
    {
        duration = fireRate;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();
		ppc = GetComponent<PlayerPowerController>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        camera = GetComponentInChildren<Camera>();

		laserLine.enabled = false;
		gunLight.enabled = false;
    }
    

    void Update () 
    {
		if(!gc.gameRunning)
			return;
        if (Input.GetButton("Fire1") && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
			ppc.addPower(-1);

            Vector3 rayOrigin = transform.position;

            RaycastHit hit;

            laserLine.SetPosition (0, gunEnd.position);
			StartCoroutine(ShotEffects());
            StartCoroutine(Shake());

            if (Physics.Raycast (rayOrigin, transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition (1, hit.point);

                Enemy health = hit.collider.GetComponent<Enemy>();

                if (health != null)
                {
                    health.Damage (gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition (1, rayOrigin + (transform.forward * weaponRange));
            }
        }
    }

	IEnumerator ShotEffects() {
		laserLine.enabled = true;
		gunLight.enabled = true;
        AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
        audio.clip = audioClip;
        audio.Play();
		yield return new WaitForSeconds(.02f);
		laserLine.enabled = false;
		gunLight.enabled = false;
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
