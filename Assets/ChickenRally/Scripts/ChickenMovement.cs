using UnityEngine;

public class ChickenMovement : MonoBehaviour
{    
	private float mhNormal;
    
	public float movementSpeed;
	public float rotationSpeed;

	private bool shouldStop = false;

	void FixedUpdate ()
	{
		mhNormal = Input.GetAxis("Mouse X");

		if (!shouldStop) {
			transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);

			#if UNITY_EDITOR
			transform.Rotate (Vector3.up, mhNormal * rotationSpeed * Time.deltaTime);
			#else
            transform.Rotate(Vector3.up,  (-1 * (Input.acceleration.x)) * rotationSpeed * Time.deltaTime);
			#endif
		}
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Sphere") {
			shouldStop = true;	
		}
	}
}