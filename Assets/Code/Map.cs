using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    public Transform LocationsContainer, LanesContainer;

    public IEnumerable<Location> Locations
    { get { return GetComponentsInChildren<Location>(); } }

    public IEnumerable<Lane> Lanes
    { get { return GetComponentsInChildren<Lane>(); } }

    public IEnumerable<Entity> Entities => GetComponentsInChildren<Entity>();
    public IEnumerable<Critter> Critters => GetComponentsInChildren<Critter>();

    public Graph Graph
    {
        get
        {
            Dictionary<Location, Graph.Node> nodes = Locations.ToDictionary(
                location => location, 
                location => new Graph.Node(new LocationData(location)));

            foreach(Lane lane in Lanes)
            {
                if (!lane.B.ContainsObstacle)
                    nodes[lane.A].AddNeighbor(nodes[lane.B]);

                if (!lane.A.ContainsObstacle)
                    nodes[lane.B].AddNeighbor(nodes[lane.A]);
            }

            return new Graph(nodes.Values);
        }
    }

    public void Add(Location location) { location.transform.SetParent(LocationsContainer); }
    public void Add(Lane lane) { lane.transform.SetParent(LanesContainer); }

    public Location GetNearestLocation(Vector3 position)
    {
        return Locations.MinElement(location => location.transform.position.Distance(position));
    }
}


public static class MapExtensions
{
    public static Graph.Node GetNode(this Graph graph, Location location)
    {
        return graph.Nodes.FirstOrDefault(
            node => node.GetLocation() == location);
    }

    public static Location GetLocation(this Graph.Node node)
    {
        return (node.Data as LocationData).Location;
    }
}

