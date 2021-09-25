using UnityEngine;
using System.Collections;
using System.Linq;


public abstract class TargetEntityAbility : Ability
{
    public EntityCursor TargetCursor;

    public Entity Target { get { return TargetCursor.Target; } }

    protected override void Update()
    {
        base.Update();

        if (Critter.IsSelected &&
            IsSelected)
        {
            TargetCursor.IsVisible = true;

            if (Target != null && InputUtility.WasMouseRightReleased)
                TryDo();
        }
        else
            TargetCursor.IsVisible = false;
    }
}
