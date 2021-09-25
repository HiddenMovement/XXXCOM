using UnityEngine;
using System.Collections;
using System.Linq;


public class Attributes : MonoBehaviour
{
    public int this[Attribute attribute]
    {
        get
        {
            int base_stat = GetComponentsInChildren<Trait>()
                .Sum(trait => trait.Bonuses[attribute]);

            float factor = 1;

            return (base_stat * factor).Round();
        }
    }

    public int GetBaseValue(Attribute attribute)
    {
        Traits traits = GetComponent<Traits>();
        int status_value = 0;
        if (traits != null && traits.Status != null)
            status_value = traits.Status.Bonuses[attribute];

        return this[attribute] - status_value;
    }
}


public enum Attribute
{
    None = 0,

    //Intrinsics
    Vitality = 1, Willpower = 2,
    Dexterity = 3, Dodge = 4,
    Energy = 5, Vigilance = 6,
    Speed = 7,

    //Maladies
    Exhaustion = 8, Anguish = 9,
    Terror = 10,
    Arousal = 11,

    //Inventory
    Armor = 12, Bullets = 13, Grenades = 14
}
