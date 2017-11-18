using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChickenMovement : MonoBehaviour
{
	private Animator animator;

	public GameObject[] hats;
	public Transform hatOrigin;

	private float vNormal;
	private float hNormal;
	private float mvNormal;
	private float mhNormal;
	private float lNormal;
	private float rNormal;

	public float movementSpeed;
	public float rotationSpeed;

	public Transform gun;
	public Transform spawnPoint;
	public GameObject bullet;
	public float bulletSpeed;
	public Text bulletText;
	public Text scoreText;

	public int bulletCount;
	public int score;

	public Text debug;

	private void Start()
	{
		animator = GetComponent<Animator>();
		bulletText.text = bulletCount.ToString();
		scoreText.text = score.ToString();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
		case "Collectable":
			Addhat(other.GetComponent<Collectable>().id);
			Destroy(other.gameObject);
			break;
		case "Ammo":
			bulletCount += 100;
			bulletText.text = bulletCount.ToString();
			Destroy(other.gameObject);
			break;
		case "Bullet":
			AddBullet();
			Destroy(other.gameObject);
			break;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && bulletCount > 0)
		{
			GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
			newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;

			bulletCount--;
			bulletText.text = bulletCount.ToString();

			Destroy(newBullet, 2.0f);
		}
	}

	void FixedUpdate ()
	{
		vNormal = Input.GetAxis("Vertical");
		hNormal = Input.GetAxis("Horizontal");

		mvNormal = Input.GetAxis("Mouse Y");
		mhNormal = Input.GetAxis("Mouse X");

		animator.SetFloat("InputV", 1);
		animator.SetFloat("InputH", hNormal);

		transform.Translate(Vector3.forward * 1 * movementSpeed * Time.deltaTime);
		transform.Translate(Vector3.right * hNormal * movementSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up,  (-1 * Input.acceleration.x) * rotationSpeed * Time.deltaTime);

		gun.Rotate(Vector3.right, -mvNormal * rotationSpeed * Time.deltaTime);
	}

	public void Addhat(int id)
	{
		foreach (Transform child in hatOrigin)
		{
			Destroy(child.gameObject);
		}

		GameObject newHat = Instantiate(hats[id], hatOrigin);
		newHat.transform.position = hatOrigin.position;
	}

	public void AddBullet()
	{
		bulletCount++;
		bulletText.text = bulletCount.ToString();
	}

	public void AddScore()
	{
		score += 10;
		scoreText.text = score.ToString();
	}
}