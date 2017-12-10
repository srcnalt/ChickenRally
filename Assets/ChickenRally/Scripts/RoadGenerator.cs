using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public MeshFilter roadA;
    public MeshFilter roadB;
    [Space(10)]

    public MeshFilter groundA;
    public MeshFilter groundB;
    [Space(10)]

    public Transform[] obstacleList;
    public Transform roadSide;
    public Transform[] trees;

    public Transform obstacles;

    Transform tree;
    Transform side;

    public int numberOfRoadPiece;
	public float roadPieceLength;
    public float roadPieceWidth;
    public float groundPieceWidth;
    [Space(10)]

    public Material material;
    public Material materialGround;

    int[] triangles;
    Vector3[] vertices;
    Vector3[] quad = new Vector3[4];

    int[] trianglesGround;
    Vector3[] verticesGround;
    Vector3[] quadGround = new Vector3[4];

    float add = 0;
    float verticalDisplacement = 0;
    public int currentPos = 0;

    Transform roadToSwap;
    Transform groundToSwap;
    List<Transform> listToSwap;
    List<Transform> treeToSwap;

    List<Transform> roadSideObjectsA = new List<Transform>();
    List<Transform> roadSideObjectsB = new List<Transform>();

    List<Transform> treesA = new List<Transform>();
    List<Transform> treesB = new List<Transform>();

    [HideInInspector]
    public List<Transform> collectableList = new List<Transform>();

    float randomSeed;

    // Use this for initialization
    void Start ()
    {
        randomSeed = Random.Range(0f, 1000f);

        roadToSwap = roadA.transform;
        roadPieceWidth *= 0.5f;

        groundToSwap = groundA.transform;
        groundPieceWidth *= 0.5f;

        vertices = new Vector3[numberOfRoadPiece * 4];
		triangles = new int[numberOfRoadPiece * 6];

        verticesGround = new Vector3[numberOfRoadPiece * 4];
        trianglesGround = new int[numberOfRoadPiece * 6];
        
        listToSwap = roadSideObjectsA;
        treeToSwap = treesA;

        CreateRoadSideObjects(roadSideObjectsA, false);
        CreateRoadSideObjects(roadSideObjectsB, false);

        CreateRoadSideObjects(treesA, true);
        CreateRoadSideObjects(treesB, true);

        CreateRoadPiece(numberOfRoadPiece, roadA, groundA, roadSideObjectsA, treesA);
        CreateRoadPiece(numberOfRoadPiece, roadB, groundB, roadSideObjectsB, treesB);
    }

	void GetRandom ()
    { 
        float f = currentPos * 0.1f;
        add = Mathf.PerlinNoise(f + randomSeed, 0) * 50 - 25;
        verticalDisplacement = add / 2;
    }

	private void CreateRoadPiece(int n, MeshFilter road, MeshFilter ground, List<Transform> objList, List<Transform> treeList)
	{
		for (int i = 0; i < n; i++)
		{
            GetRandom();

            if (currentPos == 0)
            {
                GameObject.Find("StartGround").transform.position += new Vector3(0, verticalDisplacement + 1, 0);
            }

            if(currentPos > 0 && i == 0)
            {
                vertices[4 * i] = quad[2];
                vertices[4 * i + 1] = quad[3];

                verticesGround[4 * i] = quadGround[2];
                verticesGround[4 * i + 1] = quadGround[3];
            }
            else if (i == 0)
			{
				vertices[0] = new Vector3(-roadPieceWidth, verticalDisplacement, 0);
				vertices[1] = new Vector3(roadPieceWidth, verticalDisplacement, 0);

                verticesGround[0] = new Vector3(-groundPieceWidth, verticalDisplacement, 0);
                verticesGround[1] = new Vector3(groundPieceWidth, verticalDisplacement, 0);
            }
			else
			{
				vertices[4 * i] = vertices[4 * i - 2];
                vertices[4 * i + 1] = vertices[4 * i - 1];

                verticesGround[4 * i] = verticesGround[4 * i - 2];
                verticesGround[4 * i + 1] = verticesGround[4 * i - 1];
            }

			vertices[4 * i + 2] = new Vector3(vertices[4 * i].x + add, verticalDisplacement, roadPieceLength * (currentPos + 1));
			vertices[4 * i + 3] = new Vector3(vertices[4 * i + 1].x + add, verticalDisplacement, roadPieceLength * (currentPos + 1));

            verticesGround[4 * i + 2] = new Vector3(verticesGround[4 * i].x + add, verticalDisplacement - 0.1f, roadPieceLength * (currentPos + 1));
            verticesGround[4 * i + 3] = new Vector3(verticesGround[4 * i + 1].x + add, verticalDisplacement - 0.1f, roadPieceLength * (currentPos + 1));

            quad[0] = vertices[4 * i];
            quad[1] = vertices[4 * i + 1];
            quad[2] = vertices[4 * i + 2];
            quad[3] = vertices[4 * i + 3];

            quadGround[0] = verticesGround[4 * i];
            quadGround[1] = verticesGround[4 * i + 1];
            quadGround[2] = verticesGround[4 * i + 2];
            quadGround[3] = verticesGround[4 * i + 3];

            triangles[i * 6]        = i * 4;
			triangles[i * 6 + 1]    = i * 4 + 2;
			triangles[i * 6 + 2]    = i * 4 + 1;

			triangles[i * 6 + 3]    = i * 4 + 1;
			triangles[i * 6 + 4]    = i * 4 + 2;
			triangles[i * 6 + 5]    = i * 4 + 3;

            //ground
            trianglesGround[i * 6] = i * 4;
            trianglesGround[i * 6 + 1] = i * 4 + 2;
            trianglesGround[i * 6 + 2] = i * 4 + 1;

            trianglesGround[i * 6 + 3] = i * 4 + 1;
            trianglesGround[i * 6 + 4] = i * 4 + 2;
            trianglesGround[i * 6 + 5] = i * 4 + 3;

            if (Random.Range(1, 10) % 3 == 0 && i > 3)
            {
                Transform collectable = null;

                foreach (Transform item in collectableList)
                {
                    if (!item.gameObject.activeSelf)
                    {
                        item.gameObject.SetActive(true);
                        collectable = item;
                        break;
                    }
                }

                if (collectable == null)
                {
                    collectable = Instantiate(obstacleList[Random.Range(0, obstacleList.Length)], obstacles);
                    collectableList.Add(collectable);
                }

                collectable.position = new Vector3(Random.Range(quad[0].x + 5, quad[1].x - 5), verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2);
                collectable.rotation = collectable.rotation * Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.forward);
            }

            //left item
            side = GetOneRoadSideObject(2 * i, objList);
            side.transform.position = new Vector3(quad[0].x - (quad[0].x - quad[2].x) / 2, verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2);
            side.transform.rotation = Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[0].x - quad[2].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right);

            //right item
            side = GetOneRoadSideObject(2 * i + 1, objList);
            side.transform.position = new Vector3(quad[1].x - (quad[1].x - quad[3].x) / 2, verticalDisplacement + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2);
            side.transform.rotation = Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].x - quad[3].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right);

            //left tree
            tree = GetOneRoadSideObject(2 * i, treeList);
            tree.transform.position = new Vector3(quad[0].x - (quad[0].x - quad[2].x) / 2 - 20, verticalDisplacement - 3 + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2);
            tree.transform.rotation = Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].x - quad[3].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right);

            //right tree
            tree = GetOneRoadSideObject(2 * i + 1, treeList);
            tree.transform.position = new Vector3(quad[1].x - (quad[1].x - quad[3].x) / 2 + 40, verticalDisplacement - 2 + (quad[1].y - quad[3].y) / 2, roadPieceLength * (currentPos + 1) - roadPieceLength / 2);
            tree.transform.rotation = Quaternion.AngleAxis(-Mathf.Rad2Deg * Mathf.Atan((quad[1].x - quad[3].x) / roadPieceLength), Vector3.up) * Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan((quad[1].y - quad[3].y) / roadPieceLength), Vector3.right);

            currentPos++;
        }

        SetRoadAttributes(road, ground);
    }

    public void DisableUnseenCollectables(float z)
    {
        foreach (Transform item in collectableList)
        {
            if(item.position.z < z)
            {
                item.gameObject.SetActive(false);
            }
        }
    } 

    private void CreateRoadSideObjects(List<Transform> objList, bool isTree)
    {
        for (int i = 0; i < 2 * numberOfRoadPiece; i++)
        {
            if (isTree)
            {
                Transform roadSideObject = Instantiate(trees[Random.Range(0, trees.Length)], transform);
                objList.Add(roadSideObject);
            }
            else
            {
                Transform roadSideObject = Instantiate(roadSide, transform);
                objList.Add(roadSideObject);
            }
        }
    }

    private Transform GetOneRoadSideObject(int index, List<Transform> objList)
    {
        return objList[index];
    }

    private void SetRoadAttributes(MeshFilter road, MeshFilter ground)
    {
        road.mesh.vertices = vertices;
        road.mesh.triangles = triangles;
        road.mesh.RecalculateNormals();

        ground.mesh.vertices = verticesGround;
        ground.mesh.triangles = trianglesGround;
        ground.mesh.RecalculateNormals();

        Vector2[] UVs = new Vector2[vertices.Length];
        Vector2[] UVsGround = new Vector2[verticesGround.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            UVs[i] = new Vector2((vertices[i].x) / (roadPieceWidth * 2), vertices[i].z / roadPieceLength);
            UVsGround[i] = new Vector2((verticesGround[i].x) / (groundPieceWidth / 4), verticesGround[i].z / roadPieceLength);
        }

        road.mesh.uv = UVs;
        ground.mesh.uv = UVsGround;

        Destroy(road.gameObject.GetComponent<MeshCollider>());
        Destroy(ground.gameObject.GetComponent<MeshCollider>());

        MeshCollider coll = road.gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = road.mesh;

        coll = ground.gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = ground.mesh;

        Renderer rend = road.gameObject.GetComponent<Renderer>();
        rend.material = material;

        rend = ground.gameObject.GetComponent<Renderer>();
        rend.material = materialGround;
    }

    public void GenerateRoadExtention()
    {
        CreateRoadPiece(numberOfRoadPiece, roadToSwap.GetComponent<MeshFilter>(), groundToSwap.GetComponent<MeshFilter>(), listToSwap, treeToSwap);

        if(roadToSwap == roadA.transform)
        {
            roadToSwap = roadB.transform;
            groundToSwap = groundB.transform;
            listToSwap = roadSideObjectsB;
            treeToSwap = treesB;
        }
        else
        {
            roadToSwap = roadA.transform;
            groundToSwap = groundA.transform;
            listToSwap = roadSideObjectsA;
            treeToSwap = treesA;
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
