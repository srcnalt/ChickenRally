using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public ChickenMovement chicken;
	private Animator animator;
	private AudioSource audioSource;

	private void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bullet")
		{
			chicken.AddScore();
			animator.Play("TargetHit");
			audioSource.Play();
			Destroy(other.gameObject);
		}
	}
}
