using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMovement))]

public class ShipMainController : MonoBehaviour
{

    public ShipsManager.ShipsSide shipSide = ShipsManager.ShipsSide.A;

    private Transform localTransform;
    private bool isAlive = true;
    void Start()
    {
        localTransform = transform;

        ShipsManager.GetInstance().RegisterNewShip(shipSide, GetInstanceID(), localTransform);
    }

    public bool IsDead()
    {
        return !isAlive;
    }
}
