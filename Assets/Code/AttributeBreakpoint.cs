using UnityEngine;
using System.Collections;
using System;

public abstract class ConditionalAttributeBonuses : ConditionalTrait
{
    public AttributeDictionary ConditionalBonuses = new AttributeDictionary();

    protected override void Activate()
    {
        base.Activate();

        Bonuses.Add(ConditionalBonuses);
    }

    protected override void Disactivate()
    {
        base.Disactivate();

        Bonuses.Subtract(ConditionalBonuses);
    }
}

public class AttributeBreakpoint : ConditionalAttributeBonuses
{
    public Attribute Attribute;
    public int Breakpoint;
    public Comparison Comparison;

    protected override Func<bool> Condition =>
        () => Comparison.Evaluate(Attributes[Attribute], Breakpoint);
}