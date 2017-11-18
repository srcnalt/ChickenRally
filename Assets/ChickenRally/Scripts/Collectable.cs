using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	public int id;
	public bool rotating;

	void Update ()
	{
		if(rotating)
			transform.Rotate(Vector3.forward, Time.deltaTime * 100);
	}
}

