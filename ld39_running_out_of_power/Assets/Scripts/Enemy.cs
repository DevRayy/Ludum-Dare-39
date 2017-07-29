using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int dmg;
	public int speed;
	public Object pickup;
	private GameObject player;
	private Rigidbody rb;
    private int currentHealth;

	void Start() {
		currentHealth = (int) Random.Range(3.0f, 6.0f);
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		transform.position = new Vector3(transform.position.x,
										1,
										transform.position.z);
		transform.LookAt(player.transform);
		rb.velocity = transform.forward * speed;
		Debug.Log(rb.velocity);
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
		Instantiate(pickup, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerPowerController>().addPower(-dmg);
			Debug.Log("DMG!");
        }
    }	
	
	void OnCollisionStay (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerPowerController>().addPower(-dmg);
			Debug.Log("DMG!");
        }
    }
}
