using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
	public float rad;

	int len = 100;
    int[] triangles;

    Vector3[] vertices;
    Mesh mesh;

    float add = 0;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.name = "Road Piece";

        vertices = new Vector3[len * 4];
		triangles = new int[len * 6];

		StartCoroutine("CreateRoadPiece", len);
    }

	void GetRandom (int i)
    { 
        float f = i * 0.1f;

        add = Mathf.PerlinNoise(f, 0) * 50 - 25;
	}

	IEnumerator CreateRoadPiece(int n)
	{
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < n; i++)
		{
			GetRandom(i);

			if (i == 0)
			{
				vertices[0] = new Vector3(-rad, 0, -rad * 2);
				yield return new WaitForSeconds(0.1f);
				vertices[1] = new Vector3(rad, 0, -rad * 2);
			}
			else
			{
				vertices[4 * i + 1] = vertices[4 * i - 1];
				vertices[4 * i] = vertices[4 * i - 2];
			}

			vertices[4 * i + 2] = new Vector3(vertices[4 * i].x + add, 0, rad * i * 2);
			vertices[4 * i + 3] = new Vector3(vertices[4 * i + 1].x + add, 0, rad * i * 2);

			triangles[i * 6]        = i * 4;
			triangles[i * 6 + 1]    = i * 4 + 2;
			triangles[i * 6 + 2]    = i * 4 + 1;
			triangles[i * 6 + 3]    = i * 4 + 1;
			triangles[i * 6 + 4]    = i * 4 + 2;
			triangles[i * 6 + 5]    = i * 4 + 3;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;

        MeshCollider coll = gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = mesh;
    }

    /*
	private void OnDrawGizmos()
	{
		if (vertices == null) return;

		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
    */
}
