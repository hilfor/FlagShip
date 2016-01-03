using UnityEngine;

public class TurretRotationBehaviour : MonoBehaviour
{


    public float rotationSpeed = 1f;

    public Transform defaultParentSource;
    public Transform defaultRotationTransform;

    private Transform currentTarget;
    private Transform localTransform;
    public bool onTarget = false;



    void Start()
    {
        if (defaultParentSource)
        {
            defaultRotationTransform = defaultParentSource;
        }
        else
        {
            defaultRotationTransform = transform;
        }
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
            newRotation = GetNewRotation(defaultRotationTransform.forward);
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

}
