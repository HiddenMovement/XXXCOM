using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;


public class PointerWatcher : MonoBehaviour
{
    public GameObject TouchedGameObject { get; private set; }
    public IEnumerable<GameObject> HoveredGameObjects { get; private set; }

    private void Update()
    {
        PointerEventData pointer_event_data = new PointerEventData(null);
        List<RaycastResult> raycast_results = new List<RaycastResult>();

        pointer_event_data.position = Input.mousePosition;
        The.TacticalGraphicRaycaster.Raycast(pointer_event_data, raycast_results);

        if (raycast_results.Count > 0)
            TouchedGameObject = raycast_results.First().gameObject;

        HoveredGameObjects = raycast_results.Select(raycast_result => raycast_result.gameObject);
    }
}
