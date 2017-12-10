using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    private float mhNormal;
    private GameObject col;

    public float movementSpeed;
    public float rotationSpeed;
    public RoadGenerator road;

	void FixedUpdate()
    {
        if (UIManager.instance.menuOn) return;

        mhNormal = Input.GetAxis("Mouse X");

        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        #if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
            transform.Rotate(Vector3.up, mhNormal * rotationSpeed * Time.deltaTime);
        #elif UNITY_ANDROID
            transform.Rotate(Vector3.up, ((Input.acceleration.x)) * rotationSpeed * 4 * Time.deltaTime);
        #else
            transform.Rotate(Vector3.up,  (-1 * (Input.acceleration.x)) * rotationSpeed * Time.deltaTime);
        #endif

        if (transform.position.z >= (road.currentPos - road.numberOfRoadPiece + 1) * road.roadPieceLength)
        {
            road.GenerateRoadExtention();
            road.DisableUnseenCollectables(transform.position.z);
        }
    }
}