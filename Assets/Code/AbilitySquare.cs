using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TraitSquare))]
public class AbilitySquare : UIElement
{
    public Ability Ability
    {
        get { return TraitSquare.Trait as Ability; }
        set { TraitSquare.Trait = value; }
    }

    public TraitSquare TraitSquare
    { get { return GetComponent<TraitSquare>(); } }

    private void Update()
    {
        TraitSquare.SelectionOverlay.gameObject
            .SetActive(Ability.IsSelected);

        if (WasClicked)
            Ability.IsSelected = !Ability.IsSelected;
    }
}
