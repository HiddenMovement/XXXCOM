using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(Resident))]
public class ResidentEditor : Editor
{
    bool is_snapping = false;

    public Resident Resident { get { return target as Resident; } }

    private void OnSceneGUI()
    {
        if (Event.current.type == EventType.KeyDown &&
            Event.current.keyCode == KeyCode.Space)
            is_snapping = true;

        if(Event.current.type == EventType.KeyUp &&
            Event.current.keyCode == KeyCode.Space)
            is_snapping = false;

        if (is_snapping)
            Snap();
    }

    void Snap()
    {
        Vector3 intersection = MathUtility.Intersect(
            HandleUtility.GUIPointToWorldRay(Event.current.mousePosition),
            new Plane(Vector3.up, The.Floor.transform.position + new Vector3(0, 1, 0)));

        Location location = The.Map.GetNearestLocation(intersection);

        Resident.Location = location;
        Resident.transform.position = location.transform.position;
    }
}
