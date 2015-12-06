using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


[RequireComponent(typeof(ProjectilesController))]
public class TargetingBehaviour : MonoBehaviour
{

    //public 

    public float maxTargetDistance;
    public ShipMainController mainController;

    //private
    private ProjectilesController pc;
    private Transform localTransform;

    private ArrayList viableTargets;
    private ArrayList validTargets;

    private Transform currentTarget;
    private ShipMainController currentTargetController = null;

    private bool onTarget = false;

    void Start()
    {
        localTransform = transform;
        pc = GetComponent<ProjectilesController>();
        //mainController = GetComponent<ShipMainController>();
    }

    // Fast iteration
    void Update()
    {
        DoStuffWithCurrentTarget();
    }

    void DoStuffWithCurrentTarget()
    {
        if (onTarget)
            pc.Shoot();
    }

    void FollowTarget()
    {

    }

    // Slow iteration
    void FixedUpdate()
    {
        UpdateViableTargets();
        UpdateValidTargets();
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
        if (validTargets.Count > 0 && currentTargetController && currentTargetController.IsDead())
        {
            currentTarget = (Transform)validTargets[0];
            currentTargetController = (ShipMainController)currentTarget.GetComponent<ShipMainController>();
        }
    }

}
