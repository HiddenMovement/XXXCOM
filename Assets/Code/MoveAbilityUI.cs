using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class MoveAbilityUI : AbilityUI<Move>
{
    Location target_location = null;

    Transform bezier_lines_container;

    public float Sharpness = 1.5f;
    public float Relaxation = 0.237f;

    public BezierLineController BezierLineControllerPrefab;
    public List<Material> HighlightMaterials = new List<Material>();

    private void Start()
    {
        bezier_lines_container = new GameObject("BezierLinesContainer").transform;
        bezier_lines_container.Expatriate(Content);
    }

    protected override void Update()
    {
        base.Update();
        if (Ability == null)
            return;

        GeneratePathVisualization();
        HighlightLocations();
    }

    void GeneratePathVisualization()
    {
        if (target_location == The.Floor.LocationPointedAt)
            return;
        target_location = The.Floor.LocationPointedAt;

        bezier_lines_container.DestroyChildren();

        List<Vector3> path = Critter.Location.GetPathTo(target_location);
        if (path == null)
            return;

        List<BezierCurve> bezier_path =
            MathUtility.GetBezierPath(path, Sharpness, Relaxation);
        foreach (BezierCurve bezier_curve in bezier_path)
        {
            BezierLineController bezier_line_controller =
                Instantiate(BezierLineControllerPrefab);
            bezier_line_controller.transform.SetParent(bezier_lines_container);
            bezier_line_controller.BezierCurve = bezier_curve;
        }
    }

    void HighlightLocations()
    {
        foreach (Location location in The.Map.Locations)
        {
            List<Vector3> path = Critter.Location.GetPathTo(location);
            if (path == null)
            {
                location.Highlight.SetActive(false);
                continue;
            }

            int moves_required = Ability.GetMovesRequired(path.Length());

            location.Highlight.SetActive(
                path != null &&
                location != Critter.Location &&
                moves_required <= Critter.Entity.Attributes[Attribute.Energy]);

            int index = Ability.MoveCount + moves_required - 1;
            if (index >= HighlightMaterials.Count)
                index = (index - (HighlightMaterials.Count - 2)) % 2 +
                        (HighlightMaterials.Count - 2);
            else if (index < 0)
                index = 0;
            location.HighlightRenderer.material = HighlightMaterials[index];
        }
    }

    protected override void OnSelected()
    {
        base.OnSelected();
    }

    protected override void OnDeselected()
    {
        base.OnDeselected();

        foreach (Location location in The.Map.Locations)
            location.Highlight.SetActive(false);
    }
}
