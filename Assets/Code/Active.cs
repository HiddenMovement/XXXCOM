using UnityEngine;
using System.Collections;


public class Active : Trait
{
    public int ActionPointsPerTurn = 0;

    protected override void Start()
    {
        base.Start();

        AddResponse<OnBeginTurn>(() => 
            Inventory[Item.ActionPoints] += ActionPointsPerTurn);
    }
}
