using System.Collections;
using UnityEngine;

public class ChangePropertyPassage : Prop.Passage
{
    public string PropertyName;

    public Query<int> DeltaQuery;
    public int Delta => DeltaQuery();

    public string Hint
    {
        get
        {
            string hint = "";

            if (Delta >= 0)
                hint += "+";
            else
                hint += "-";

            hint += Delta + " " + PropertyName;

            return hint;
        }
    }

    public override void Read()
    {
        base.Read();

        Prop.Properties[PropertyName] += DeltaQuery();

        Prop.Toaster.MakeToast(Hint, Prop.PrimaryColor);
    }
}

public static class ChangePassageExtensions
{
    public static ChangePropertyPassage Gain(this Prop prop,
                                     Query<int> delta_query,
                                     string property_name)
    {
        ChangePropertyPassage change_passage = Passage.Make<ChangePropertyPassage>();
        change_passage.Prop = prop;
        change_passage.DeltaQuery = delta_query;
        change_passage.PropertyName = property_name;

        return change_passage;
    }
    public static ChangePropertyPassage Gain(this Character character,
                                     Query<int> delta_query, 
                                     string property_name)
    {
        return character.Prop.Gain(delta_query, property_name); 
    }

    public static ChangePropertyPassage Gain(this Prop prop,
                                     int delta,
                                     string property_name)
    { 
        return prop.Gain(() => delta, property_name); 
    }
    public static ChangePropertyPassage Gain(this Character character,
                                     int delta,
                                     string property_name)
    { 
        return character.Prop.Gain(delta, property_name);
    }

    public static ChangePropertyPassage Lose(this Prop prop,
                                     Query<int> delta_query,
                                     string property_name)
    { 
        return prop.Gain(() => -delta_query(), property_name); 
    }
    public static ChangePropertyPassage Lose(this Character character,
                                     Query<int> delta_query,
                                     string property_name)
    { 
        return character.Prop.Lose(delta_query, property_name); 
    }

    public static ChangePropertyPassage Lose(this Prop prop,
                                     int delta,
                                     string property_name)
    {
        return prop.Lose(() => delta, property_name);
    }
    public static ChangePropertyPassage Lose(this Character character,
                                     int delta,
                                     string property_name)
    { 
        return character.Prop.Lose(delta, property_name); 
    }
}

