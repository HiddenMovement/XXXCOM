using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//****Rename to "World"?
public class Floor : MonoBehaviour
{
    public bool IsTouched => !The.UI.IsTouched;

    public bool WasLeftClicked => InputUtility.WasMouseLeftReleased && 
                                  IsTouched;

    public bool WasRightClicked => InputUtility.WasMouseRightReleased &&
                                   IsTouched;

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
