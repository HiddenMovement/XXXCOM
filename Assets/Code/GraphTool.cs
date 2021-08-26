using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteAlways]
public class GraphTool : MonoBehaviour
{
    public Vector3Int Resolution = new Vector3Int(1, 1, 1);

    public Map MapPrefab;
    public Location LocationPrefab;
    public Lane LanePrefab;

    public Vector3 Dimensions => transform.localScale;
    public Vector3 Corner => transform.position + Dimensions / -2;

    private void Update()
    {
        if (Application.isPlaying)
            gameObject.SetActive(false);

        transform.rotation = Quaternion.identity;
    }

    public bool IsSubsumed(Vector3 position)
    {
        return position.GreaterThan(Corner) &&
               position.LessThan(Corner + Dimensions);
    }

    public IEnumerable<Location> GetLocationsSubsumed()
    {
        return The.Map.Locations
            .Where(location => IsSubsumed(location.transform.position));
    }

    public IEnumerable<Lane> GetLanesSubsumed()
    {
        return GetLocationsSubsumed().SelectMany(location => location.Lanes);
    }
}
