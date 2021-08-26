using UnityEngine;
using System.Collections;

public abstract class TargetedAbility : Ability
{
    public Location Target;

    protected override void Update()
    {
        base.Update();

        if (Critter.IsSelected &&
            IsSelected &&
            InputUtility.WasMouseRightReleased)
        {
            Target = The.Floor.LocationPointedAt;

            TryDo();
        }
    }
}
