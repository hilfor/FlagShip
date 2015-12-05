using UnityEngine;
using System.Collections;

public class TacMapController : MonoBehaviour
{

    public Camera tacCam;

    private TacCamMovement tacCamMovementController;
    private ShipMovement selectedShip;

    void Start()
    {
        if (tacCam)
        {
            tacCamMovementController = tacCam.gameObject.GetComponent<TacCamMovement>();
        }
    }

    public void DoSelectProjection(Vector2 point)
    {
        point.x *= tacCam.pixelWidth;
        point.y *= tacCam.pixelHeight;
        RaycastHit hit;
        Ray ray = tacCam.ScreenPointToRay(point);
        Debug.DrawRay(ray.origin, ray.direction * 50);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.name);
            switch (hit.collider.tag)
            {
                case ("Ship"):
                    selectedShip = hit.collider.GetComponent<ShipMovement>();
                    tacCamMovementController.SetFocusTarget(hit.collider.transform);
                    break;
                case ("MovementPlane"):
                    MoveShip(selectedShip, hit.point);
                    break;
            }
        }
    }

    public void ClearSelection()
    {
        selectedShip = null;
    }

    protected void MoveShip(ShipMovement moveThis, Vector3 moveHere)
    {
        if (moveThis)
        {
            //Debug.Log("moving " + moveThis + " here " + moveHere);
            moveHere.y = moveThis.transform.position.y;
            moveThis.AddWaypoint(moveHere);
        }
    }

}
