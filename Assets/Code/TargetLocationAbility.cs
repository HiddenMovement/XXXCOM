using UnityEngine;
using System.Collections;


public abstract class TargetLocationAbility : Ability
{
    public LocationCursor LocationCursor;

    public Location Target => LocationCursor.Location;

    protected override void Update()
    {
        base.Update();

        if (Critter.IsSelected &&
            IsSelected)
        {
            LocationCursor.IsVisible = true;

            if (The.Floor.WasRightClicked)
                TryDo();
        }
        else
            LocationCursor.IsVisible = false;
    }
}