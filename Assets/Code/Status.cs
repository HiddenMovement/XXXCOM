using UnityEngine;
using System.Collections;


public class Status : Trait
{
    public void Gain(Attribute attribute, int value)
    {
        if (attribute == Attribute.Vitality && value < 0)
            Triggers.OnTakeDamage(value);

        Bonuses[attribute] += value;
    }

    public void Lose(Attribute attribute, int value)
    {
        Gain(attribute, -value);
    }

    public void Set(Attribute attribute, int value)
    {
        Bonuses[attribute] = value;
    }

    public void Clear(Attribute attribute)
    {
        Set(attribute, 0);
    }

    public void Gain(AttributeDictionary attributes)
    {
        foreach (Attribute attribute in attributes.Keys)
            Gain(attribute, attributes[attribute]);
    }

    public void Lose(AttributeDictionary attributes)
    {
        foreach (Attribute attribute in attributes.Keys)
            Lose(attribute, attributes[attribute]);
    }
}
