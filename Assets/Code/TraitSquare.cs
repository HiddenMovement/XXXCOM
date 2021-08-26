using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TraitSquare : UIElement
{
    public Trait Trait;

    public Image Icon;
    public Image SelectionOverlay;

    public Button Button { get { return GetComponent<Button>(); } }

    private void Update()
    {
        Icon.sprite = Trait.Icon;
    }
}
