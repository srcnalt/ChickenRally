using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public MeshFilter roadA;
    public MeshFilter roadB;

    public int numberOfRoadPiece;
    public float roadPieceLength;
    public float roadPieceWidth;
    public Material material;
    
    int[] triangles;

    Vector3[] vertices;
    Vector3[] lastVerts = new Vector3[2];

    float add = 0;
    public int currentPos = 0;

    Transform roadToSwap;

	// Use this for initialization
	void Start ()
    {
        roadToSwap = roadA.transform;

        vertices = new Vector3[numberOfRoadPiece * 4];
		triangles = new int[numberOfRoadPiece * 6];
        
        CreateRoadPiece(numberOfRoadPiece, roadA);
        CreateRoadPiece(numberOfRoadPiece, roadB);
    }

	void GetRandom ()
    { 
        float f = currentPos * 0.1f;
        add = Mathf.PerlinNoise(f, 0) * 50 - 25;
	}

	private void CreateRoadPiece(int n, MeshFilter road)
	{
		for (int i = 0; i < n; i++)
		{
            GetRandom();

            if(currentPos > 0 && i == 0)
            {
                vertices[4 * i] = lastVerts[0];
                vertices[4 * i + 1] = lastVerts[1];
            }
            else if (i == 0)
			{
				vertices[0] = new Vector3(-roadPieceWidth, 0, 0);
				vertices[1] = new Vector3(roadPieceWidth, 0, 0);
			}
			else
			{
				vertices[4 * i] = vertices[4 * i - 2];
                vertices[4 * i + 1] = vertices[4 * i - 1];
            }

			vertices[4 * i + 2] = new Vector3(vertices[4 * i].x + add, 0, roadPieceLength * (currentPos + 1));
			vertices[4 * i + 3] = new Vector3(vertices[4 * i + 1].x + add, 0, roadPieceLength * (currentPos + 1));

            lastVerts[0] = vertices[4 * i + 2];
            lastVerts[1] = vertices[4 * i + 3];

			triangles[i * 6]        = i * 4;
			triangles[i * 6 + 1]    = i * 4 + 2;
			triangles[i * 6 + 2]    = i * 4 + 1;

			triangles[i * 6 + 3]    = i * 4 + 1;
			triangles[i * 6 + 4]    = i * 4 + 2;
			triangles[i * 6 + 5]    = i * 4 + 3;

            currentPos++;
        }

        SetRoadAttributes(road);
    }

    private void SetRoadAttributes(MeshFilter road)
    {
        road.mesh.vertices = vertices;
        road.mesh.triangles = triangles;
        road.mesh.RecalculateNormals();

        Vector2[] UVs = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            UVs[i] = new Vector2(vertices[i].x / roadPieceWidth, vertices[i].z / roadPieceLength);
        }

        road.mesh.uv = UVs;

        Destroy(road.gameObject.GetComponent<MeshCollider>());

        MeshCollider coll = road.gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = road.mesh;

        Renderer rend = road.gameObject.GetComponent<Renderer>();
        rend.material = material;
    }

    public void GenerateRoadExtention()
    {
        CreateRoadPiece(numberOfRoadPiece, roadToSwap.GetComponent<MeshFilter>());

        if(roadToSwap == roadA.transform)
        {
            roadToSwap = roadB.transform;
        }
        else
        {
            roadToSwap = roadA.transform;
        }
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
