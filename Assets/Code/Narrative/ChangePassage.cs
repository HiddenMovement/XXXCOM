using System.Collections;
using UnityEngine;

//****naming
public class ChangePassage : Prop.Passage
{
    public string Property;//****"PropertyName", elsewhere

    public Query<int> DeltaQuery;
    public int Delta => DeltaQuery();//****needed? naming?

    public string Hint
    {
        get
        {
            string hint = "";

            if (Delta >= 0)
                hint += "+";
            else
                hint += "-";

            hint += Delta + " " + Property;

            return hint;
        }
    }

    public override void Read()
    {
        base.Read();

        Prop.Properties[Property] += DeltaQuery();

        Prop.Toaster.MakeToast(Hint, Prop.PrimaryColor);
    }
}

public static class ChangePassageExtensions
{
    public static ChangePassage Gain(this Prop prop,
                                     Query<int> value_query,//****deltaquery?
                                     string property)
    {
        ChangePassage change_passage = Passage.Make<ChangePassage>();
        change_passage.Prop = prop;
        change_passage.DeltaQuery = value_query;
        change_passage.Property = property;

        return change_passage;
    }
    public static ChangePassage Gain(this Character character,
                                     Query<int> value_query, 
                                     string property)
    { 
        return character.Prop.Gain(value_query, property); 
    }

    public static ChangePassage Gain(this Prop prop,
                                     int value,
                                     string property)
    { 
        return prop.Gain(() => value, property); 
    }
    public static ChangePassage Gain(this Character character,
                                     int value,
                                     string property)
    { 
        return character.Prop.Gain(value, property);
    }

    public static ChangePassage Lose(this Prop prop,
                                     Query<int> value_query,
                                     string property)
    { 
        return prop.Gain(() => -value_query(), property); 
    }
    public static ChangePassage Lose(this Character character,
                                     Query<int> value_query,
                                     string property)
    { 
        return character.Prop.Lose(value_query, property); 
    }

    public static ChangePassage Lose(this Prop prop,
                                     int value,
                                     string property)
    {
        return prop.Lose(() => value, property);
    }
    public static ChangePassage Lose(this Character character,
                                     int value,
                                     string property)
    { 
        return character.Prop.Lose(value, property); 
    }
}

