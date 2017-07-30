using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticleController : MonoBehaviour {

	public float timeToDestroyAfter;
	public AudioClip audioClip;
	public GameObject audioPlayer;
	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyAfterTime());
	}
	
	IEnumerator DestroyAfterTime() {
		yield return new WaitForSeconds(timeToDestroyAfter + Random.Range(0.0f, 1.0f));
		Destroy(this.gameObject);
	}

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Floor")
        {
			 AudioSource audio = ((GameObject) Instantiate(audioPlayer, transform.position, transform.rotation)).GetComponent<AudioSource>();
			 audio.clip = audioClip;
			 audio.Play();
        }
    }	
}
