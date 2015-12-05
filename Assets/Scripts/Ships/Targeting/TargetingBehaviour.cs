using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


[RequireComponent(typeof(ProjectilesController))]
[RequireComponent(typeof(ShipMainController))]
public class TargetingBehaviour : MonoBehaviour
{

    //public 

    public float maxTargetDistance;

    //private
    private ProjectilesController pc;
    private Transform localTransform;
    private ShipMainController mainController;

    private ArrayList viableTargets;
    private ArrayList validTargets;

    private Transform currentTarget;

    private bool onTarget = false;

    void Start()
    {
        localTransform = transform;
        pc = GetComponent<ProjectilesController>();
        mainController = GetComponent<ShipMainController>();
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
        viableTargets = ShipsManager.GetInstance().GetShipsInRange(localTransform.position, localTransform.forward, mainController.side, maxTargetDistance);
    }

    void UpdateValidTargets()
    {
        validTargets = viableTargets;
    }

    void UpdateCurrentTarget()
    {
        if (validTargets.Count > 0)
        {
            currentTarget = (Transform)validTargets[0];
        }
    }

}
