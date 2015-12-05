using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMovement))]

public class ShipMainController : MonoBehaviour
{

    public ShipsManager.ShipsSide side = ShipsManager.ShipsSide.A;
    private Transform localTransform;
    void Start()
    {
        localTransform = transform;

        ShipsManager.GetInstance().RegisterNewShip(side, GetInstanceID(), localTransform);
    }
}
