using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(GraphTool))]
public class GraphToolEditor : Editor
{
    public GraphTool GraphTool { get { return target as GraphTool; } }

    public Vector3Int Resolution => GraphTool.Resolution;
    public Vector3 Dimensions => GraphTool.Dimensions;
    public Vector3 Corner => GraphTool.Corner;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            Map map = UnityEditor.PrefabUtility
                .InstantiatePrefab(GraphTool.MapPrefab) as Map;

            for (int x_index = 0; x_index < Resolution.x; x_index++)
            {
                float x = Corner.x + ((float)x_index / Resolution.x) * Dimensions.x;

                for (int y_index = 0; y_index < Resolution.y; y_index++)
                {
                    float y = Corner.y + ((float)y_index / Resolution.y) * Dimensions.y;

                    for (int z_index = 0; z_index < Resolution.z; z_index++)
                    {
                        float z = Corner.z + ((float)z_index / Resolution.z) * Dimensions.z;

                        Location location = UnityEditor.PrefabUtility
                            .InstantiatePrefab(GraphTool.LocationPrefab) as Location;
                        map.Add(location);
                        location.transform.position = new Vector3(x, y, z);
                    }
                }
            }

            foreach (Location location in map.Locations)
                foreach (Location other_location in map.Locations)
                    AddLane(location, other_location);
        }

        if (GUILayout.Button("Prune Lanes"))
        {
            IEnumerable<Location> obstacle_locations = GraphTool.GetLocationsSubsumed()
                .Where(location => location.Residents
                    .FirstOrDefault(resident => !resident.HasComponent<Critter>() && 
                                                resident.HasComponent<Obstacle>()));

            foreach (Location location in obstacle_locations)
                foreach (Lane lane in GraphTool.GetLanesSubsumed())
                    if (lane.LineSegment.Distance(location.transform.position) < 1)
                        DestroyImmediate(lane.gameObject);
        }

        if (GUILayout.Button("Remove Lanes"))
        {
            foreach (Lane lane in GraphTool.GetLanesSubsumed())
                DestroyImmediate(lane.gameObject);
        }

        if (GUILayout.Button("Add Lanes"))
        {
            IEnumerable<Location> locations = GraphTool.GetLocationsSubsumed();

            foreach (Location location in locations)
                foreach (Location other_location in locations)
                    AddLane(location, other_location);
        }
    }

    void AddLane(Location a, Location b)
    {
        if (a == b)
            return;

        Lane existing_lane =
            a.Lanes.FirstOrDefault(lane_ => lane_.B == b);
        if (existing_lane != null)
            return;
        existing_lane =
            b.Lanes.FirstOrDefault(lane_ => lane_.B == a);
        if (existing_lane != null)
            return;

        float distance = a.transform.position
                        .Distance(b.transform.position);
        if (distance > Mathf.Sqrt(2))
            return;

        Lane lane = UnityEditor.PrefabUtility
                .InstantiatePrefab(GraphTool.LanePrefab) as Lane;
        a.Map.Add(lane);
        lane.A = a;
        lane.B = b;
    }
}
