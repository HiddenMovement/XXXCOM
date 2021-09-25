using UnityEngine;
using System.Collections;

public class Mind : BodyPart
{
    protected override void Update()
    {
        base.Update();

        Type = BodyPartType.Control;
        Size = 0;
    }
}
