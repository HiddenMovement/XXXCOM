using UnityEngine;
using System.Collections;

public class Vigilant : Trait
{
    public int MaximumVigilanceGain = 1;

    protected override void Start()
    {
        base.Start();

        AddResponse<OnBeginTurn>(() =>
        {
            Status.Clear(Attribute.Vigilance);
        });

        AddResponse<OnEndTurn>(() =>
        {
            int vigilance_gain = Mathf.Min(Attributes[Attribute.Energy], 
                                           MaximumVigilanceGain);

            Status.Gain(Attribute.Vigilance, vigilance_gain);
            Status.Lose(Attribute.Energy, vigilance_gain);
        });
    }
}
