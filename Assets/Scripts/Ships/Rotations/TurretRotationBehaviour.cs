using UnityEngine;

public class TurretRotationBehaviour : MonoBehaviour
{


    public float rotationSpeed = 1f;

    public Vector3 defaultRotation;

    private Transform currentTarget;
    private Transform localTransform;
    public bool onTarget = false;



    void Start()
    {
        defaultRotation = transform.forward;
        localTransform = transform;
    }

    public void FollowTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }

    public bool IsOnTarget()
    {

        return onTarget;
    }

    void Update()
    {
        LockOnTarget();
    }

    void LockOnTarget()
    {
        Quaternion newRotation;
        if (currentTarget)
        {
            newRotation = Utils.GetNewLookAtPoint(currentTarget.position, localTransform.position, localTransform.forward, rotationSpeed);
            if (!localTransform.rotation.Equals(newRotation))
            {
                //Debug.Log("Following target " + currentTarget);
                onTarget = false;
                localTransform.rotation = newRotation;
            }
            else
            {
                //Debug.Log("Currently on target");
                onTarget = true;
            }
        }
        else
        {
            newRotation = GetNewRotation(defaultRotation);
            if (!localTransform.rotation.Equals(newRotation))
            {
                //Debug.Log("Returning to default rotation");
                onTarget = false;
                localTransform.rotation = newRotation;
            }
        }

    }

    Quaternion GetNewRotation(Vector3 lookAt)
    {
        return Quaternion.LookRotation(lookAt);
    }

    //Quaternion GetNewLookAtPoint(Vector3 whereTo)
    //{
    //    Vector3 targetDirection = whereTo - localTransform.position;
    //    float step = rotationSpeed * Time.deltaTime;
    //    Vector3 newDirection = Vector3.RotateTowards(localTransform.forward, targetDirection, step, 0f);
    //    return Quaternion.LookRotation(newDirection);
    //}
}
