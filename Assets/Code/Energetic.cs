using UnityEngine;
using System.Collections;


public class Energetic : Trait
{
    protected override void Start()
    {
        base.Start();

        AddResponse<OnBeginTurn>(() => Status.Clear(Attribute.Energy));
    }
}
