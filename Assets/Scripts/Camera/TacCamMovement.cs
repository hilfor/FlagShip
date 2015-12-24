using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class TacCamMovement : MonoBehaviour
{

    public Transform focusTarget;
    public Transform defaultFocusTarget;
    [Range(0, 1)]
    public float rotaionSpeed = .5f;

    public float focusMargin = 30f;

    public float frustumHeight;
    public float frustumWidth;

    public float requiredAngle;

    public float maxZoom = 5f;
    public float maxZoomSpeed;

    public Transform defaultCameraPosition;
    public Transform defaultCameraRotationDirection;


    private bool onTarget = false;
    private Transform localTransform;
    private Camera localCamera;

    private bool smoothFocusOnTarget = false;

    #region defaults

    private Vector3 defaultRotationDirection;
    private Vector3 defaultPositioning;
    private float defaultFov;
    #endregion

    private float aspectRatio;
    private float fov;
    private float tanFov;

    void Start()
    {
        localTransform = transform;
        localCamera = GetComponent<Camera>();

        SetDefaults();

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
            //TestFrustum();
        }
    }

    void ResetView()
    {
        localCamera.fieldOfView = defaultFov;
        localTransform.rotation = Quaternion.FromToRotation(localTransform.forward, defaultRotationDirection);
    }

    void RotateTowardsTarget()
    {

        // Should I smooth rotate towards the target or just follow it whereever it goes (smooth focus is slower rotation)
        if (smoothFocusOnTarget)
        {
            // Smooth rotation towards the target
            Quaternion newRotation = Utils.GetNewLookAtPoint(focusTarget.position, localTransform.position, localTransform.forward, rotaionSpeed);
            if (!localTransform.rotation.Equals(newRotation))
            {
                onTarget = false;
                localTransform.rotation = newRotation;
            }
            else
            {
                onTarget = true;
                smoothFocusOnTarget = false;
            }
        }
        else
        {
            // Just look at the target 
            localTransform.LookAt(focusTarget);
        }

    }

    void ZoomOnTarget()
    {

        float distanceFromObject = (focusTarget.position - localTransform.position).magnitude;
        localCamera.fieldOfView = Mathf.Rad2Deg * Mathf.Tan(focusMargin / distanceFromObject);

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
        smoothFocusOnTarget = true;
    }

    public void SetFocusTarget(Transform focusTarget)
    {
        this.focusTarget = focusTarget;
        smoothFocusOnTarget = true;
    }

    #endregion

    public void ClearFocusTarget()
    {
        if (defaultFocusTarget)
        {
            focusTarget = defaultFocusTarget;
        }
        else
        {
            focusTarget = null;
        }
        smoothFocusOnTarget = true;
        ResetView();


    }

    void SetDefaults()
    {
        if (defaultCameraRotationDirection)
        {
            defaultRotationDirection = defaultCameraRotationDirection.position;
        }
        else
        {
            defaultRotationDirection = localTransform.forward;
        }

        if (defaultCameraPosition)
        {
            defaultPositioning = defaultCameraPosition.position;
        }
        else
        {
            defaultPositioning = localTransform.position;
        }

        defaultFov = localCamera.fieldOfView;
    }
}
