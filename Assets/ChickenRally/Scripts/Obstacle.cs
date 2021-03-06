﻿using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    private GameObject touchedObject;

    public void OnTriggerEnter(Collider col)
    {
        touchedObject = col.gameObject;

        React();
    }

    public void React()
    {
        UIManager.instance.DisplayGameOverText();

        touchedObject.GetComponent<ChickenMovement>().movementSpeed = 0;
        touchedObject.GetComponent<ChickenMovement>().rotationSpeed = 0;
        touchedObject.GetComponent<Animator>().SetBool("Stop", true);

        touchedObject.GetComponent<AudioSource>().clip = touchedObject.GetComponent<ChickenMovement>().die;
        touchedObject.GetComponent<AudioSource>().Play();

        UIManager.instance.gameOver.GetComponent<Animator>().Play("GameOver");

        StartCoroutine(Camera.main.GetComponent<CameraMovement>().Shake());

        Cursor.lockState = CursorLockMode.Locked;
    }
}
