using System.Collections;
using UnityEngine;

public class ChangePassage : Prop.Passage
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
    public static ChangePassage Gain(this Prop prop,
                                     Query<int> delta_query,
                                     string property_name)
    {
        ChangePassage change_passage = Passage.Make<ChangePassage>();
        change_passage.Prop = prop;
        change_passage.DeltaQuery = delta_query;
        change_passage.PropertyName = property_name;

        return change_passage;
    }
    public static ChangePassage Gain(this Character character,
                                     Query<int> delta_query, 
                                     string property_name)
    {
        return character.Prop.Gain(delta_query, property_name); 
    }

    public static ChangePassage Gain(this Prop prop,
                                     int delta,
                                     string property_name)
    { 
        return prop.Gain(() => delta, property_name); 
    }
    public static ChangePassage Gain(this Character character,
                                     int delta,
                                     string property_name)
    { 
        return character.Prop.Gain(delta, property_name);
    }

    public static ChangePassage Lose(this Prop prop,
                                     Query<int> delta_query,
                                     string property_name)
    { 
        return prop.Gain(() => -delta_query(), property_name); 
    }
    public static ChangePassage Lose(this Character character,
                                     Query<int> delta_query,
                                     string property_name)
    { 
        return character.Prop.Lose(delta_query, property_name); 
    }

    public static ChangePassage Lose(this Prop prop,
                                     int delta,
                                     string property_name)
    {
        return prop.Lose(() => delta, property_name);
    }
    public static ChangePassage Lose(this Character character,
                                     int delta,
                                     string property_name)
    { 
        return character.Prop.Lose(delta, property_name); 
    }
}

