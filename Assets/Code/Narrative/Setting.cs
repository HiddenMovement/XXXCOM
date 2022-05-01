using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Prop))]
[RequireComponent(typeof(PropVisualization))]
public class Setting : MonoBehaviour
{
    public Prop Prop => GetComponent<Prop>();
    public PropVisualization PropVisualization => 
        GetComponent<PropVisualization>();

    private void Update()
    {
        RectTransform display_rect_transform = 
            The.NarrativeUI.transform as RectTransform;
        float display_aspect_ratio =
            display_rect_transform.rect.width /
            display_rect_transform.rect.height;

        float background_aspect_ratio =
            PropVisualization.Image.sprite.rect.width /
            PropVisualization.Image.sprite.rect.height;

        float width, height;
        if(background_aspect_ratio > display_aspect_ratio)
        {
            height = display_rect_transform.rect.height;
            width = height * background_aspect_ratio;
        }
        else
        {
            width = display_rect_transform.rect.width;
            height = width / background_aspect_ratio;
        }

        (transform as RectTransform).sizeDelta = new Vector2(width, height);
    }
}
