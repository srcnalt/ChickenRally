using UnityEngine;

public class Collectable : MonoBehaviour, ICollidable
{
    private GameObject touchedObject;

    public void OnCollisionEnter(Collision col)
    {
        touchedObject = col.gameObject;
    }

    public void React()
    {

    }
}
