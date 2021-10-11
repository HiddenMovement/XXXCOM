using UnityEngine;
using System.Collections;
using System.Linq;


[RequireComponent(typeof(Resident))]
public class MapCursor : MonoBehaviour
{
    Vector3 smooth_visualization_position;

    public float AnimationSpeed = 1;

    public GameObject Visualization;
    public bool IsVisible
    {
        get { return Visualization.activeSelf; }
        set { Visualization.SetActive(value); }
    }

    public Location Location
    {
        get { return GetComponent<Resident>().Location; }
        set { GetComponent<Resident>().Location = value; }
    }

    protected virtual void Start()
    {
        Location = The.Map.Locations.First();
    }

    protected virtual void Update()
    {
        Visualization.transform.position = 
        smooth_visualization_position = 
            smooth_visualization_position.Lerped(
                transform.position, 
                AnimationSpeed * 8 * Time.deltaTime);
    }
}