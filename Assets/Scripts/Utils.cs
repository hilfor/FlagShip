using UnityEngine;
using System.Collections;

public class Utils
{
    public static Quaternion GetNewLookAtPoint(Vector3 whereTo, Vector3 currentPosition, Vector3 direction, float speed)
    {
        Vector3 targetDirection = whereTo - currentPosition;
        float step = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(direction, targetDirection, step, 0f);
        return Quaternion.LookRotation(newDirection);
    }
}
