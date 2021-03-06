﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {

	public Transform chicken;
	public Vector3 offset;
	public float camSpeed;

    public float shakeAmount;
    public float shakeDuration;

    void Start()
    {
        #if UNITY_IOS || UNITY_ANDROID
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        #else
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        #endif
    }

    void FixedUpdate ()
	{
		transform.position = chicken.TransformPoint(offset);
		transform.rotation = Quaternion.Lerp(transform.rotation, chicken.rotation, Time.deltaTime * camSpeed);
	}

    public IEnumerator Shake()
    {
        while (shakeDuration > 0)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;

            shakeDuration -= Time.deltaTime;

            Camera.main.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime);

            yield return null;
        }
        
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Game");
    }
}
