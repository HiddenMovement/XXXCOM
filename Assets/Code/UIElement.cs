using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ClickDetector))]
public class UIElement : RectElement
{
    ClickDetector ClickDetector
    { get { return GetComponent<ClickDetector>(); } }

    public Vector2Int PixelPosition { get { return RectTransform.anchoredPosition.Round(); } }

    public virtual bool IsPointedAt { get { return this.IsPointedAt(); } }
    public virtual bool IsTouched { get { return this.IsTouched(); } }

    public bool IsPressed { get { return ClickDetector.IsPressed; } }
    public bool WasClicked { get { return ClickDetector.WasClicked; } }
}
