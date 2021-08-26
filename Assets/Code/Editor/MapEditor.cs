using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    public Map Map { get { return target as Map; } }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate names"))
        {
            int i = 0;
            foreach (Location location in Map.Locations)
                location.name = "Location " + (i++).ToString();

            foreach (Lane lane in Map.Lanes)
                lane.name = lane.A.name + " to " + lane.B.name;
        }
    }
}
