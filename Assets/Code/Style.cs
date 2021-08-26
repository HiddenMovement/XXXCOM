using UnityEngine;
using System.Collections;

[ExecuteAlways]
public class Style : MonoBehaviour
{
    public bool ShowDebugOverlay = false;

    private void Update()
    {
        if (Application.isPlaying)
            ShowDebugOverlay = false;
    }
}
