using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public MeshFilter roadA;
    public MeshFilter roadB;

    public Transform obstacle;
    public Transform roadSide;

    public int numberOfRoadPiece;
    public float roadPieceLength;
    public float roadPieceWidth;
    public Material material;
    
    int[] triangles;

    Vector3[] vertices;
    Vector3[] quad = new Vector3[4];

    float add = 0;
    float verticalDisplacement = 0;
    public int currentPos = 0;

    Transform roadToSwap;
    Transform[] obstacles;

	// Use this for initialization
	void Start ()
    {
        roadToSwap = roadA.transform;
        roadPieceWidth *= 0.5f;

        vertices = new Vector3[numberOfRoadPiece * 4];
		triangles = new int[numberOfRoadPiece * 6];
        
        CreateRoadPiece(numberOfRoadPiece, roadA);
        CreateRoadPiece(numberOfRoadPiece, roadB);
    }

	void GetRandom ()
    { 
        float f = currentPos * 0.1f;
        add = Mathf.PerlinNoise(f, 0) * 50 - 25;
        verticalDisplacement = add / 2;

    }

	private void CreateRoadPiece(int n, MeshFilter road)
	{
		for (int i = 0; i < n; i++)
		{
            GetRandom();

            if(currentPos > 0 && i == 0)
            {
                vertices[4 * i] = quad[2];
                vertices[4 * i + 1] = quad[3];
            }
            else if (i == 0)
			{
				vertices[0] = new Vector3(-roadPieceWidth, verticalDisplacement, 0);
				vertices[1] = new Vector3(roadPieceWidth, verticalDisplacement, 0);
			}
			else
			{
				vertices[4 * i] = vertices[4 * i - 2];
                vertices[4 * i + 1] = vertices[4 * i - 1];
            }

			vertices[4 * i + 2] = new Vector3(vertices[4 * i].x + add, verticalDisplacement, roadPieceLength * (currentPos + 1));
			vertices[4 * i + 3] = new Vector3(vertices[4 * i + 1].x + add, verticalDisplacement, roadPieceLength * (currentPos + 1));

            quad[0] = vertices[4 * i];
            quad[1] = vertices[4 * i + 1];
            quad[2] = vertices[4 * i + 2];
            quad[3] = vertices[4 * i + 3];

            triangles[i * 6]        = i * 4;
			triangles[i * 6 + 1]    = i * 4 + 2;
			triangles[i * 6 + 2]    = i * 4 + 1;

			triangles[i * 6 + 3]    = i * 4 + 1;
			triangles[i * 6 + 4]    = i * 4 + 2;
			triangles[i * 6 + 5]    = i * 4 + 3;

            //add these to object pooler

            if (Random.Range(1, 10) % 4 == 0)
            {
                Instantiate(
                        obstacle,
                        new Vector3(Random.Range(quad[0].x + 5, quad[1].x - 5), verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2),
                        obstacle.rotation * Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.forward));
            }

            Instantiate(
                        roadSide, 
                        new Vector3(quad[0].x - (quad[0].x - quad[2].x) / 2, verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2),
                        Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[0].x - quad[2].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right));
            
            Instantiate(
                        roadSide,
                        new Vector3(quad[1].x - (quad[1].x - quad[3].x) / 2, verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2),
                        Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].x - quad[3].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right));

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
            UVs[i] = new Vector2((vertices[i].x) / (roadPieceWidth * 2), vertices[i].z / roadPieceLength);
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
