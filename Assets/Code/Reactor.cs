using UnityEngine;
using System.Collections;

public class Reactor : Trait
{
    protected override void Start()
    {
        base.Start();

        AddResponse<OnEndTurn>(() =>
        {
            Inventory[Item.ReactionPoints] =
                Inventory[Item.ActionPoints];

            Inventory[Item.ActionPoints] = 0;
        });
    }
}
