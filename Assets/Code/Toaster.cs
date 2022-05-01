using System.Collections;
using UnityEngine;


public class Toaster : RectElement
{
    public Toast ToastPrefab;

    public Toast MakeToast(string message)
    {
        Toast toast = Instantiate(ToastPrefab);
        toast.Text.text = message;

        toast.transform.SetParent(transform);
        toast.transform.localPosition =
            new Vector2(Random.value * Rect.width,
                        Random.value * Rect.height);

        return toast;
    }

    public Toast MakeToast(string message, Color color)
    {
        Toast toast = MakeToast(message);
        toast.Color = color;

        return toast;
    }
}