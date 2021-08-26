using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Resident : MonoBehaviour
{
    public Location Location
    {
        get { return GetComponentInParent<Location>(); }
        set { value.AddResident(this); }
    }
}
