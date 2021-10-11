using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteAlways]
public class Location : MonoBehaviour
{
    public Transform ResidentsContainer, DebugContainer;

    public GameObject Highlight;
    public Renderer HighlightRenderer;

    public Entity Entity =>
        ResidentsContainer.GetComponentInChildren<Entity>();
    public bool ContainsEntity { get { return Entity != null; } }

    public Critter Critter =>
        ResidentsContainer.GetComponentInChildren<Critter>();
    public bool ContainsCritter { get { return Critter != null; } }

    public Map Map
    { get { return GetComponentInParent<Map>(); } }

    public IEnumerable<Lane> Lanes
    {
        get
        {
            return Map.Lanes.Where(lane => lane.A == this || 
                                             lane.B == this);
        }
    }

    public IEnumerable<Resident> Residents
    { get { return ResidentsContainer.GetComponentsInChildren<Resident>(); } }

    public bool ContainsObstacle => 
        ResidentsContainer.GetComponentInChildren<Obstacle>() != null;

    private void Update()
    {
        DebugContainer.gameObject.SetActive(The.Style.ShowDebugOverlay);
    }

    public bool Contains(Resident resident)
    {
        return ResidentsContainer.Children().Contains(resident.transform);
    }

    public void AddResident(Resident resident)
    {
        resident.transform.SetParent(ResidentsContainer);
        resident.transform.position = transform.position;
    }

    public List<Vector3> GetPathTo(Location target)
    {
        Graph graph = Map.Graph;
        Graph.Node A = graph.GetNode(this),
                   B = graph.GetNode(target);

        Graph.Path path = A.GetPathTo_Euclidean(B);
        if (path == null)
            return null;

        return path.Select(node => node.GetPosition()).ToList();
    }
}


public class LocationData : Graph.PositionData
{
    public Location Location { get; private set; }

    public override Vector3 Position
    {
        get => Location.transform.position;
        set => Location.transform.position = value;
    }

    public LocationData(Location location)
    {
        Location = location;
    }
}
