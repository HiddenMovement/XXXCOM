using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Vector3 PositionPointedAt
    {
        get
        {
            return MathUtility.Intersect(
                The.TacticalCamera.ScreenPointToRay(Input.mousePosition), 
                new Plane(Vector3.up, transform.position));
        }
    }

    public Location LocationPointedAt
    {
        get
        {
            return The.Map.Locations
                .MinElement(location => location.transform.position
                    .Distance(PositionPointedAt));
        }
    }
}
