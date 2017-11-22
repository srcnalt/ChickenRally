using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenCollision : MonoBehaviour {
	public Text gameOver;
	public float shakeAmount;
	public float shakeDuration;

	float shakePercentage;
	float startAmount;
	float startDuration;

	bool isRunning = false;	

	public bool smooth;
	public float smoothAmount = 5f;

	public void ShakeCamera(float amount, float duration) {

		shakeAmount += amount;
		startAmount = shakeAmount;
		shakeDuration += duration;
		startDuration = shakeDuration;

		if(!isRunning) StartCoroutine (Shake());
	}


	IEnumerator Shake() {
		isRunning = true;

		while (shakeDuration > 0.01f) {
			Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
			rotationAmount.z = 0;

			shakePercentage = shakeDuration / startDuration;

			shakeAmount = startAmount * shakePercentage;
			shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);

			if(smooth)
				transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
			else
				transform.localRotation = Quaternion.Euler (rotationAmount);

			yield return null;
		}
		transform.localRotation = Quaternion.identity;
		isRunning = false;
	}

	void OnCollisionEnter(Collision col) {
		//Bonus points
		if (col.gameObject.tag == "Bonus") {
			Destroy (col.gameObject);
		}
        else if (col.gameObject.tag == "Obstacle") {
			gameOver.gameObject.SetActive(true);
			ShakeCamera (2.5f, 2.0f);

            Destroy(col.gameObject);

            var animator = gameObject.GetComponent<Animator> ();
			animator.SetBool ("Stop", true);
		}
	}

}
