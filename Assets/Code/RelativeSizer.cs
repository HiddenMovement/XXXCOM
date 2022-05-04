using UnityEngine;
using System.Collections;

[ExecuteAlways]
public class RelativeSizer : MonoBehaviour
{
    public RectTransform Reference;
    public bool ScaleHorizontally = false;
    public float RelativeHorizontalSize = 1,
                 ExtraHorizontalSize = 0;
    public bool ScaleVertically = false;
    public float RelativeVerticalSize = 1, 
                 ExtraVerticalSize = 0;

    public RectTransform RectTransform { get { return transform as RectTransform; } }

    void Start()
    {

    }

    void Update()
    {
        if (Reference == null)
            return;

        float horizontal_size = 
            Reference.rect.size.x * 
            RelativeHorizontalSize +
            ExtraHorizontalSize;

        float vertical_size =
            Reference.rect.size.y *
            RelativeVerticalSize +
            ExtraVerticalSize;

        RectTransform.sizeDelta =
            new Vector2(ScaleHorizontally ? horizontal_size : 
                                            RectTransform.sizeDelta.x,
                        ScaleVertically ? vertical_size :
                                          RectTransform.sizeDelta.y);
    }
}
