using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticleController : MonoBehaviour {

	public float timeToDestroyAfter;
	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyAfterTime());
	}
	
	IEnumerator DestroyAfterTime() {
		yield return new WaitForSeconds(timeToDestroyAfter + Random.Range(0.0f, 1.0f));
		Destroy(this.gameObject);
	}
}
