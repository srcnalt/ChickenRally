using UnityEngine;

public interface ICollidable
{
    void OnCollisionEnter(Collision col);

    void React();
}
