using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class BodyPart : Trait
{
    public BodyPartType Type;
    public int Size = 1;
    public int Importance = 1;

    public AttributeDictionary Recovery = new AttributeDictionary();

    public int Function =>
        10 - Wounds.Sum(wound => wound.FunctionLoss);

    public virtual bool IsCrippled
    { get { return Function > 0; } }

    public IEnumerable<Wound> Wounds
    { get { return GetComponentsInChildren<Wound>(); } }

    protected override void Start()
    {
        base.Start();

        AddResponse<OnBeginTurn>(() =>
        {
            if (!IsCrippled)
                foreach (Attribute attribute in Recovery.Keys)
                    if ((Recovery[attribute] > 0 && Status.Bonuses[attribute] < 0) ||
                        (Recovery[attribute] < 0 && Status.Bonuses[attribute] > 0))
                        Status.Gain(attribute, Recovery[attribute]);
                
        });
    }

    public void Injure(Wound wound)
    {
        wound.transform.SetParent(transform);
    }
}

public enum BodyPartType
{
    None,
    Locomotion,
    Manipulation,
    Sensing,
    LifeSupport,
    Control
}