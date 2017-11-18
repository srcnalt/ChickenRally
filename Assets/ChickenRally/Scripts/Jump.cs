using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	private Rigidbody rb;
	private Animator animator;

	public float amount;
	public float fallMultiplier;
	public float additionalGravity;

	private float jumpDelay = 0.3f;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.Play("Jump");
			Invoke("StartJump", jumpDelay);
			Invoke("ChangeAnim", 1.5f);
		}

		if(rb.velocity.y < 0)
		{
			rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
		}

		if(rb.velocity.y != 0)
		{
			rb.velocity -= Vector3.up * additionalGravity * Time.deltaTime;
		}
	}

	public void StartJump()
	{
		rb.velocity = Vector3.up * amount;
	}

	public void ChangeAnim()
	{
		animator.Play("Run-Stand");
	}
}

