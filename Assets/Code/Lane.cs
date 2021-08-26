using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Lane : MonoBehaviour
{
    public Location A, B;

    public LineRenderer LineRenderer;
    public Transform DebugContainer;

    public LineSegment LineSegment
    {
        get
        {
            return new LineSegment(A.transform.position, 
                                   B.transform.position);
        }
    }

    private void Update()
    {
        if (A == null || B == null)
            return;

        LineRenderer.SetPosition(0, A.transform.position);
        LineRenderer.SetPosition(1, B.transform.position);

        DebugContainer.gameObject.SetActive(The.Style.ShowDebugOverlay);
    }
}   
