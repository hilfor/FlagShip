using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{


    public Queue positionsQueue;


    [Range(0, 1)]
    public float positionError = 0.3f;

    [Range(1, 20)]
    public float maxMovementSpeed = 7.0f;
    [Range(1, 10)]
    public float maxRotationSpeed = 3.0f;

    public Vector3 velocity = Vector3.zero;

    public Vector3 initialPositioning = Vector3.up;

    public bool waitUntilFullyRotated = false;

    private Vector3 nextPosition;
    private Transform localTransform;
    private bool firstWaypointNotSet = true;
    private bool onTarget = false;

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
        if (IsNextPositionValid())
        {
            RotateTowardsNextPosition();
            if (waitUntilFullyRotated)
            {
                if (onTarget)
                {
                    Move();
                }
            }
            else
            {
                Move();
            }
        }
    }

    public void AddWaypoint(Vector3 newPosition)
    {
        positionsQueue.Enqueue(newPosition);
    }

    private void Move()
    {
        transform.position = Vector3.SmoothDamp(localTransform.position, nextPosition, ref velocity, maxMovementSpeed);
    }

    private void RotateTowardsNextPosition()
    {
        Quaternion newRotation = Utils.GetNewLookAtPoint(nextPosition, localTransform.position, localTransform.forward, maxRotationSpeed);
        if (!localTransform.rotation.Equals(newRotation))
        {
            onTarget = false;
            localTransform.rotation = newRotation;
        }
        else
        {
            onTarget = true;
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
