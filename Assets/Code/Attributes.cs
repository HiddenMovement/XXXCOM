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
                .Sum(trait => trait.AttributesBonuses[attribute]);

            float factor = 1;

            return (base_stat * factor).Round();
        }
    }
}

public enum Attribute
{
    Health,
    DamageTaken,
    Speed,
    Dodge,
    Aim,
    Will
}
