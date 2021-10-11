using UnityEngine;
using System.Collections;
using System;

public class Disability : ConditionalAttributeBonuses
{
    public int Breakpoint;

    protected override Func<bool> Condition =>
        () => Comparison.LessThanOrEqual.Evaluate(Part.Function, Breakpoint);

    public BodyPart Part { get { return GetComponentInParent<BodyPart>(); } }
}
