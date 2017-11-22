using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    private float mhNormal;

    public float movementSpeed;
    public float rotationSpeed;
    public RoadGenerator road;

    private bool shouldStop = false;

    void FixedUpdate()
    {
        mhNormal = Input.GetAxis("Mouse X");

        if (!shouldStop)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            #if UNITY_EDITOR
                transform.Rotate(Vector3.up, mhNormal * rotationSpeed * Time.deltaTime);
            #else
                transform.Rotate(Vector3.up,  (-1 * (Input.acceleration.x)) * rotationSpeed * Time.deltaTime);
            #endif

            if (transform.position.z >= (road.currentPos - road.numberOfRoadPiece + 1) * road.roadPieceLength)
            {
                Debug.Log("Passed");

                road.GenerateRoadExtention();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            shouldStop = true;
        }
    }
}