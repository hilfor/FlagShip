using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ShipsManager
{
    public enum ShipsSide
    {
        A,
        B
    }

    private Hashtable shipsCollection;
    private static ShipsManager instance = null;


    public static ShipsManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ShipsManager();
        }
        return instance;
    }

    private ShipsManager()
    {
        shipsCollection = new Hashtable();

    }


    public void RegisterNewShip(ShipsSide side, int shipId, Transform shipTransform)
    {
        if (!shipsCollection.ContainsKey(side))
        {
            shipsCollection.Add(side, new Hashtable());
        }

        Hashtable sideHash = (Hashtable)shipsCollection[side];

        if (!sideHash.ContainsKey(shipId))
        {
            sideHash.Add(shipId, shipTransform);
        }

    }

    public void DeregisterShip(ShipsSide side, int shipId)
    {
        if (shipsCollection.ContainsKey(side))
        {
            Hashtable sideHash = (Hashtable)shipsCollection[side];
            if (sideHash.ContainsKey(shipId))
            {
                sideHash.Remove(shipId);
            }

        }
    }

    public ArrayList GetShipsInRange(Vector3 sourcePosition, Vector3 destination, ShipsSide shipSide, float maxDistance)
    {
        ArrayList ships = new ArrayList();

        foreach (ShipsSide side in shipsCollection.Keys)
        {
            if (side == shipSide)
                continue;

            foreach (Transform shipTransform in ((Hashtable)shipsCollection[side]).Values)
            {
                float distance = Vector3.Distance(sourcePosition, shipTransform.position);
                float angle = Vector3.Angle(shipTransform.position, destination);
                if (distance <= maxDistance && angle < 90)
                {
                    ships.Add(shipTransform);
                }
            }
        }

        return ships;

    }

}
