using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Turn
{
    public bool HasBegun { get; private set; } = false;
    public bool HasEnded { get; private set; } = false;
    public abstract bool IsReadyToEnd { get; }

    public virtual void Begin() { HasBegun = true; }
    public virtual void End() { HasEnded = true; }
}
