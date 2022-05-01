using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : UIElement
{
    public Canvas Canvas => GetComponentInParent<Canvas>();
}
