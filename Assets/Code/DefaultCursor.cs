using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(LocationCursor))]
public class DefaultCursor : MonoBehaviour
{
    public LocationCursor LocationCursor => GetComponent<LocationCursor>();

    private void Update()
    {
        LocationCursor.IsVisible = The.Map.GetComponentsInChildren<MapCursor>()
            .AllTrue(map_cursor => !map_cursor.IsVisible || 
                                   map_cursor.HasComponent<DefaultCursor>());
    }
}
