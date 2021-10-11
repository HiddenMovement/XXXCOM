using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Weighted))]
public class Wound : Trait
{
    public int FunctionLoss = 0;
    public AttributeDictionary Bleed = new AttributeDictionary();

    public bool HasBeenTreated = false;

    protected override void Start()
    {
        base.Start();

        AddResponse<OnEndTurn>(() => Status.Gain(Bleed));
    }
}
