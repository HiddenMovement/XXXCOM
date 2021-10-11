using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class ConditionalTrait : Trait
{
    protected abstract Func<bool> Condition { get; }

    public bool IsActive { get; private set; }

    protected override void Update()
    {
        base.Update();

        if (Condition())
        {
            if (!IsActive)
                Activate();
        }
        else
        {
            if (IsActive)
                Disactivate();
        }
    }

    protected virtual void Activate() { IsActive = true; }
    protected virtual void Disactivate() { IsActive = false; }
}