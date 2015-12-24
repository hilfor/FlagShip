using UnityEngine;
using System.Collections;

public class AngleRotator : MonoBehaviour
{

    public enum Axis
    {
        X,
        Y,
        Z
    }

    public Axis[] rotateOnAxises;
    public float[] steps;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rotateOnAxises.Length; i++)
        {
            Axis axis = rotateOnAxises[i];
            switch (axis)
            {
                case Axis.X:
                    transform.Rotate(Vector3.right, steps[i]);
                    break;
                case Axis.Y:
                    transform.Rotate(Vector3.up, steps[i]);
                    break;
                case Axis.Z:
                    transform.Rotate(Vector3.forward, steps[i]);
                    break;
            }
        }
    }
}
