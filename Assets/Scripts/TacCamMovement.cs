using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class TacCamMovement : MonoBehaviour
{

    public Transform focusTarget;
    public Transform defaultFocusTarget;
    public float rotaionSpeed = 3f;

    private Transform localTransform;
    private Camera localCamera;
    void Start()
    {
        localTransform = transform;
        localCamera = GetComponent<Camera>();
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
        }
    }

    void RotateTowardsTarget()
    {

        //localTransform.LookAt(Vector3.Lerp(localTransform.eulerAngles,focusTarget.eulerAngles, Time.deltaTime*rotaionSpeed));
        localTransform.LookAt(focusTarget);

        //localTransform.rotation = Quaternion.RotateTowards(localTransform.rotation, focusTarget.rotation, 3);
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
}
