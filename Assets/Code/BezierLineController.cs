using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteAlways]
public class BezierLineController : MonoBehaviour
{
    [SerializeField]
    LineRenderer line_renderer = null;

    public BezierCurve BezierCurve;

    public PathType Path = PathType.Cubic; 
    public int SampleCount = 10;

    void Start()
    {
        SetPositions();
    }

    void Update()
    {
        SetPositions();
    }

    void SetPositions()
    {
        if (Path == PathType.Linear)
        {
            line_renderer.positionCount = 2;
            line_renderer.SetPosition(0, BezierCurve.A);
            line_renderer.SetPosition(1, BezierCurve.B);
            return;
        }

        int effective_sample_count = Mathf.Max(1, (SampleCount - 1));

        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i <= effective_sample_count; i++)
            positions.Add(GetPositionAlongPath(i * 1.0f / effective_sample_count));

        line_renderer.positionCount = positions.Count;
        line_renderer.SetPositions(positions.ToArray());
    }

    public Vector3 GetPositionAlongPath(float t)
    {
        switch(Path)
        {
            case PathType.Linear:
                return BezierCurve.A.Lerped(BezierCurve.B, t);

            case PathType.Quadratic:
                return Mathf.Pow(1 - t, 2) * BezierCurve.A +
                       2 * (1 - t) * t * BezierCurve.ControlPointA +
                       t * t * BezierCurve.B;

            case PathType.Cubic:
                return Mathf.Pow(1 - t, 3) * BezierCurve.A +
                       3 * Mathf.Pow(1 - t, 2) * t * BezierCurve.ControlPointA +
                       3 * (1 - t) * t * t * BezierCurve.ControlPointB +
                       t * t * t * BezierCurve.B;

            default: return Vector3.zero;
        }
    }

    int TToIndex(float t)
    {
        return (int)(t * (SampleCount - 2));
    }

    public Vector3 GetTangent(int index)
    {
        return line_renderer.GetPosition(index + 1) - line_renderer.GetPosition(index);
    }

    public Vector3 GetTangent(float t)
    {
        return GetTangent(TToIndex(t));
    }

    public Vector3 GetTangentInScreenSpace(Camera camera, int index)
    {
        return camera.WorldToScreenPoint(line_renderer.GetPosition(index + 1)) -
               camera.WorldToScreenPoint(line_renderer.GetPosition(index));
    }

    public Vector3 GetTangentInScreenSpace(float t)
    {
        return GetTangentInScreenSpace(TToIndex(t));
    }

    public float GetDistance(Vector3 position)
    {
        if (Path == PathType.Linear)
            return position.Distance(new LineSegment(BezierCurve.A, BezierCurve.B));

        return Enumerable.Range(0, line_renderer.positionCount - 1)
            .Select(index => position.Distance(new LineSegment(line_renderer.GetPosition(index), 
                                                               line_renderer.GetPosition(index + 1))))
            .Min();
    }

    public float GetDistanceInScreenSpace(Camera camera, Vector3 position)
    {
        if (Path == PathType.Linear)
            return position.Distance(new LineSegment(camera.WorldToScreenPoint(BezierCurve.A), 
                                                     camera.WorldToScreenPoint(BezierCurve.B)));

        return Enumerable.Range(0, line_renderer.positionCount - 1)
            .Select(index => position.Distance(
                new LineSegment(camera.WorldToScreenPoint(line_renderer.GetPosition(index)),
                                camera.WorldToScreenPoint(line_renderer.GetPosition(index + 1)))))
            .Min();
    }


    public enum PathType { Linear, Quadratic, Cubic }
}
