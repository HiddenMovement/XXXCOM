using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ActionPointsUI : CritterUIElement
{
    public Text Text;

    private void Update()
    {
        Text.text =
            "AP: " +
            Critter.Inventory[Item.ActionPoints];
    }
}
