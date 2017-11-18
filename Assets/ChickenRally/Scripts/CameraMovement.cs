using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public Transform chicken;
	public Vector3 offset;
	public float camSpeed;

	void FixedUpdate ()
	{
		transform.position = chicken.TransformPoint(offset);
		transform.rotation = Quaternion.Lerp(transform.rotation, chicken.rotation, Time.deltaTime * camSpeed);
	}
}
