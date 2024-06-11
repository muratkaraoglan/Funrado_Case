using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 DirectonToVector3(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                {
                    return Vector3.forward;
                }
            case Direction.Right:
                {
                    return Vector3.right;
                }
            case Direction.Left:
                {
                    return Vector3.left;
                }
            case Direction.Back:
                {
                    return Vector3.back;
                }
        }
        return Vector3.zero;
    }
}
