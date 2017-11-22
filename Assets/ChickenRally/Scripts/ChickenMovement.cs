using UnityEngine;

public class ChickenMovement : MonoBehaviour
{    
	private float mhNormal;
    
	public float movementSpeed;
	public float rotationSpeed;

	void FixedUpdate ()
	{
		mhNormal = Input.GetAxis("Mouse X");

		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        #if UNITY_EDITOR
            transform.Rotate(Vector3.up,  mhNormal * rotationSpeed * Time.deltaTime);
        #else
            transform.Rotate(Vector3.up,  (-1 * (Input.acceleration.x)) * rotationSpeed * Time.deltaTime);
        #endif
    }
}