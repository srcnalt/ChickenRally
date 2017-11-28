using UnityEngine;

public interface ICollidable
{
    void OnTriggerEnter(Collider col);

    void React();
}
