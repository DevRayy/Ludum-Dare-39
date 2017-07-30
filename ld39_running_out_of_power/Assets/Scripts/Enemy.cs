using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int dmg;
	public int speed;
	[Range(0, 1)]
	public float pickupChance;
	public Object pickup;
	public Object particleObject;
	public float timeToDestroyAfter;
	private GameObject player;
	private Rigidbody rb;
    private int currentHealth;
	private GameController gc;
	public AudioClip audioClip;
	public AudioClip audioClip2;
	public GameObject audioPlayer;

	void Start() {
		currentHealth = (int) Random.Range(3.0f, 6.0f);
		player = GameObject.FindGameObjectWithTag("Player");
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		rb = GetComponent<Rigidbody>();
		StartCoroutine(DestroyAfterTime());
	}

	void FixedUpdate() {
		if(!gc.gameRunning)
			return;
		transform.position = new Vector3(transform.position.x,
										1,
										transform.position.z);
		if(player != null) {
			transform.LookAt(player.transform);
			rb.velocity = transform.forward * speed;
		} else {
			transform.RotateAround(transform.position, transform.up, 100.0f * Time.deltaTime);
		}
	}

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) 
        {
            Kill();
        }
    }

	private void Kill() {
		if(Random.Range(0f, 1f) < pickupChance)
			Instantiate(pickup, transform.position, transform.rotation);
		AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
		audio.clip = audioClip2;
		audio.Play();
		InstantiateParticles();
		Destroy(this.gameObject);
	}

	private void InstantiateParticles() {
		for(int i=-2; i<2; i++) {
			for(int j=-2; j<2; j++) {
				for(int k=-2; k<2; k++) {
					Vector3 pos = new Vector3(transform.position.x + i*0.1f,
											transform.position.y + j*0.1f,
											transform.position.z + k*0.1f);
					GameObject particle = (GameObject) Instantiate(particleObject, pos, transform.rotation);
					particle.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 10);
				}
			}
		}
	}

	IEnumerator DestroyAfterTime() {
		yield return new WaitForSeconds(timeToDestroyAfter + Random.Range(0.0f, 1.0f));
		if(Random.Range(0f, 1f) < 0.1f)
			Instantiate(pickup, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerPowerController>().addPower(-dmg);
			 AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
			 audio.clip = audioClip;
			 audio.Play();
        }
    }	
	
	void OnCollisionStay (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerPowerController>().addPower(-dmg);
        }
    }
}
