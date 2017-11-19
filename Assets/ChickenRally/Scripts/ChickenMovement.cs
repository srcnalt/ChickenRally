using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
	private Animator animator;

	private float vNormal;
	private float hNormal;
	private float mvNormal;
	private float mhNormal;

	public float movementSpeed;
	public float rotationSpeed;

	private void Start()
	{
        animator = GetComponent<Animator>();
	}

	void FixedUpdate ()
	{
		vNormal = Input.GetAxis("Vertical");
		hNormal = Input.GetAxis("Horizontal");

		mvNormal = Input.GetAxis("Mouse Y");
		mhNormal = Input.GetAxis("Mouse X");

		animator.SetFloat("InputV", 1);
		animator.SetFloat("InputH", mhNormal);

		transform.Translate(Vector3.forward * 1 * movementSpeed * Time.deltaTime);
		transform.Translate(Vector3.right * mhNormal * movementSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up,  (-1 * Input.acceleration.x) * rotationSpeed * Time.deltaTime);
	}
}