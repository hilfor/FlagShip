using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class TacCamMovement : MonoBehaviour
{

    public Transform focusTarget;
    public Transform defaultFocusTarget;
    [Range(0, 1)]
    public float rotaionSpeed = .5f;

    public float frustumHeight;
    public float frustumWidth;

    public float requiredAngle;

    private bool onTarget = false;
    private Transform localTransform;
    private Camera localCamera;

    private Vector3 defaultRotationDirection;

    void Start()
    {
        localTransform = transform;
        localCamera = GetComponent<Camera>();
        defaultRotationDirection = localTransform.forward;
    }

    void Update()
    {
        FocusOnTarget();
    }

    void FocusOnTarget()
    {
        if (focusTarget)
        {
            RotateTowardsTarget();
            ZoomOnTarget();
            TestFrustum();
        }
    }

    void RotateTowardsTarget()
    {
        // this should be executed on the 1st focus on target 
        Quaternion newRotation = Utils.GetNewLookAtPoint(focusTarget.position, localTransform.position, localTransform.forward, rotaionSpeed);
        if (!localTransform.rotation.Equals(newRotation))
        {
            onTarget = false;
            localTransform.rotation = newRotation;
        }
        else
        {
            onTarget = true;
        }

        // this should be used after the 1st focus on target completed
        // transform.LookAt(focusTarget);
    }

    void ZoomOnTarget()
    {

        if (localCamera.orthographic)
        {
            //The camera is in ortho mode, therefore:
            // Play with the camera size 
        }
        else
        {

            //The camera is in perspective mode, therefore
            // Play with the camera view angle
        }
    }

    void TestFrustum()
    {
        RaycastHit hit;

        if (Physics.Raycast(localTransform.position, focusTarget.transform.position - localTransform.position, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Ship")
            {
                // calculate furtum height 
                frustumHeight = 2.0f * hit.distance * Mathf.Tan((float)(localCamera.fieldOfView * 0.5 * Mathf.Deg2Rad));
                frustumWidth = frustumHeight * localCamera.aspect;
                requiredAngle = 2 * Mathf.Atan((float)(frustumHeight * 0.5 / hit.distance)) * Mathf.Rad2Deg;
            }
        }
    }

    #region setGet
    public void SetFocusTarget(GameObject focusTarget)
    {
        this.focusTarget = focusTarget.transform;
    }

    public void SetFocusTarget(Transform focusTarget)
    {
        this.focusTarget = focusTarget;
    }

    public void ClearFocusTarget()
    {
        this.focusTarget = defaultFocusTarget;
    }
    #endregion
}
