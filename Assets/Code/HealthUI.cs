using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : CritterUIElement
{
    public Text Text;

    private void Update()
    {
        Text.text = 
            "HP: " + 
            Critter.Entity.Attributes[Attribute.Vitality] + 
            "/" + 
            Critter.Entity.Attributes.GetBaseValue(Attribute.Vitality);
    }
}
