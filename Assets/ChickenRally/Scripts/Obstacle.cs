using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    private GameObject touchedObject;

    public void OnCollisionEnter(Collision col)
    {
        touchedObject = col.gameObject;

        React();
    }

    public void React()
    {
        UIManager.instance.DisplayGameOverText();

        touchedObject.GetComponent<ChickenMovement>().movementSpeed = 0;
        touchedObject.GetComponent<Animator>().SetBool("Stop", true);

        StartCoroutine(Camera.main.GetComponent<CameraMovement>().Shake());
    }
}
