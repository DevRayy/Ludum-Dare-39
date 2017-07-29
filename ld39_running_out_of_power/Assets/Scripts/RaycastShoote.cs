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
                                            
    //private AudioSource gunAudio;                                       // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
	private PlayerPowerController ppc;


    void Start () 
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();
		ppc = GetComponent<PlayerPowerController>();

		laserLine.enabled = false;
		gunLight.enabled = false;
    }
    

    void Update () 
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
			ppc.addPower(-1);

            Vector3 rayOrigin = transform.position;

            RaycastHit hit;

            laserLine.SetPosition (0, gunEnd.position);
			StartCoroutine(ShotEffects());

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
		yield return new WaitForSeconds(.02f);
		laserLine.enabled = false;
		gunLight.enabled = false;
	}
}
