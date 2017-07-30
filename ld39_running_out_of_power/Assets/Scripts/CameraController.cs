using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float duration;
	private float magnitude;
	public void StartShaking(float duration, float magnitude) {
		this.duration = duration;
		this.magnitude = magnitude;
		StartCoroutine(Shake());
	}

	IEnumerator Shake() {  
        float elapsed = 0.0f;
        
        Vector3 originalCamPos = transform.localPosition;
        
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
            
            transform.localPosition = new Vector3(x, y, z) + transform.localPosition;
                
            yield return null;
        }
        
        transform.localPosition = originalCamPos;
    }
}
