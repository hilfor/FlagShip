using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{


    public Queue positionsQueue;
    public Vector3 nextPosition = Vector3.up;

    [Range(0, 1)]
    public float positionError = 0.3f;

    [Range(0, 1)]
    public float maxSpeed = 0.5f;

    public float sumMove = 1f;

    public Vector3 velocity = Vector3.zero;

    private Transform localTransform;


    void Start()
    {
        positionsQueue = new Queue();
        localTransform = transform;
    }

    void FixedUpdate()
    {
        SetNextPosition();
        Move();
    }

    public void AddWaypoint(Vector3 newPosition)
    {
        positionsQueue.Enqueue(newPosition);
    }

    private void Move()
    {

        if (nextPosition != Vector3.up)
        {
            transform.position = Vector3.SmoothDamp(localTransform.position, nextPosition, ref velocity, maxSpeed);
            //sumMove += Time.deltaTime * movementSpeed;
            //localTransform.position = Vector3.LerpUnclamped(localTransform.position, nextPosition, sumMove);
            //Debug.Log(sumMove);
        }
    }

    private void SetNextPosition()
    {


        if (Mathf.Abs((nextPosition - localTransform.position).magnitude) < positionError && positionsQueue.Count >= 1)
        {
            //Debug.Log("Popping next position");
            nextPosition = (Vector3)positionsQueue.Dequeue();
            //Debug.Log("Next position poped");
            positionsQueue.TrimToSize();
            sumMove = 0f;
        }
    }
}
