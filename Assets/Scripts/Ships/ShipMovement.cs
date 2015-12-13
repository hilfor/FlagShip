using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{


    public Queue positionsQueue;


    [Range(0, 1)]
    public float positionError = 0.3f;

    [Range(0, 1)]
    public float maxMovementSpeed = 0.5f;
    [Range(0, 1)]
    public float maxRotationSpeed = 0.5f;

    public Vector3 velocity = Vector3.zero;

    public Vector3 initialPositioning = Vector3.up;


    private Vector3 nextPosition;
    private Transform localTransform;
    private bool firstWaypointNotSet = true;

    void Start()
    {
        nextPosition = initialPositioning;
        positionsQueue = new Queue();
        localTransform = transform;
    }

    public void Initiate(Vector3 newInitialPosition)
    {
        initialPositioning = newInitialPosition;
        Start();
    }

    void FixedUpdate()
    {
        SetNextPosition();
        RotateTowardsNextPosition();
        Move();
    }

    public void AddWaypoint(Vector3 newPosition)
    {
        positionsQueue.Enqueue(newPosition);
    }

    private void Move()
    {

        if (IsNextPositionValid())
        {
            transform.position = Vector3.SmoothDamp(localTransform.position, nextPosition, ref velocity, maxMovementSpeed);
        }
    }

    private void RotateTowardsNextPosition()
    {
        if (IsNextPositionValid())
        {
            Quaternion newRotation = Utils.GetNewLookAtPoint(nextPosition, localTransform.position, localTransform.forward, maxRotationSpeed);
            if (!localTransform.rotation.Equals(newRotation))
            {
                localTransform.rotation = newRotation;
            }
        }
    }

    private void SetNextPosition()
    {

        if ((firstWaypointNotSet || Mathf.Abs((nextPosition - localTransform.position).magnitude) < positionError) && positionsQueue.Count >= 1)
        {
            nextPosition = (Vector3)positionsQueue.Dequeue();
            positionsQueue.TrimToSize();
            firstWaypointNotSet = false;
        }
    }

    private bool IsNextPositionValid()
    {
        return !nextPosition.Equals(initialPositioning);
    }

}
