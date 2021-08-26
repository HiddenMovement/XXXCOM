using UnityEngine;
using System.Collections;

public class SwellingUIElement : UIElement
{
    public float SwellFactor = 1.1f;
    public float Speed = 10;

    private void Update()
    {
        Vector3 target_scale = Vector3.one;
        if (IsTouched)
            target_scale *= SwellFactor;

        transform.localScale = transform.localScale.Lerped(
            target_scale, 
            Time.deltaTime * Speed);
    }
}
