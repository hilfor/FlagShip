using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ProjectilesController))]
[RequireComponent(typeof(TurretRotationBehaviour))]
public class TargetingBehaviour : MonoBehaviour
{

    //public 
    public float maxTargetDistance;
    public ShipMainController mainController;

    //private
    private ProjectilesController pc;
    private TurretRotationBehaviour trb;
    private Transform localTransform;

    private ArrayList viableTargets;
    private ArrayList validTargets;

    public Transform currentTarget;
    private ShipMainController currentTargetController = null;

    void Start()
    {
        localTransform = transform;
        pc = GetComponent<ProjectilesController>();
        trb = GetComponent<TurretRotationBehaviour>();
    }

    #region Fast Iteration
    void Update()
    {
        DoStuffWithCurrentTarget();
    }

    void DoStuffWithCurrentTarget()
    {

        if (trb.IsOnTarget())
            pc.Shoot();
    }
    #endregion

    #region Slow Iteration
    void FixedUpdate()
    {
        UpdateViableTargets();
        UpdateValidTargets();
        UpdateCurrentTarget();
    }


    void UpdateViableTargets()
    {
        if (mainController)
        {
            viableTargets = ShipsManager.GetInstance().GetShipsInRange(localTransform.position, localTransform.forward, mainController.shipSide, maxTargetDistance);
        }
        else
        {
            Debug.LogError("Main controller was not set on " + gameObject.name);
        }
    }

    void UpdateValidTargets()
    {
        validTargets = viableTargets;
    }

    void UpdateCurrentTarget()
    {
        if (validTargets.Count > 0)
        {
            if (currentTargetController)
            {
                if (currentTargetController.IsDead())
                {
                    Debug.Log("Setting new target, old one is dead");
                    SetNewTarget((Transform)validTargets[0]);
                }
            }
            else
            {
                Debug.Log("Setting new target (no previous target)");
                SetNewTarget((Transform)validTargets[0]);
            }
        }
    }

    void SetNewTarget(Transform newTarget)
    {
        Debug.Log("This is the new target " + newTarget.name);
        currentTarget = newTarget;
        trb.FollowTarget(currentTarget);
        currentTargetController = (ShipMainController)currentTarget.GetComponent<ShipMainController>();
    }
    #endregion
}
