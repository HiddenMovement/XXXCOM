using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Body : Trait
{
    public IEnumerable<BodyPart> Parts
    { get { return GetComponentsInChildren<BodyPart>(); } }

    protected override void Start()
    {
        base.Start();

        if (Parts.Count() == 0)
            new GameObject("Part")
                .AddComponent<BodyPart>()
                .transform.SetParent(transform);
    }

    public void Injure(Wound wound)
    {
        MathUtility.ChooseAtRandom(Parts, part => part.Size).Injure(wound);
    }

    public int GetFunction(BodyPartType type)
    {
        return Parts.WeightedAverage(
            part => part.Function, 
            part => part.Importance).Round();
    }

    public bool IsCrippled(BodyPartType type)
    {
        return Parts.All(part => part.IsCrippled);
    }
}