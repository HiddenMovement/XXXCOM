using System;
using UnityEngine;


public class Prop : MonoBehaviour
{
    public string Name, ShortName;
    public Color PrimaryColor, SecondaryColor;

    public Properties Properties = new Properties();
    public PropertyQuery this[string property] =>
        new PropertyQuery(this, property);

    public PropState State;

    public Toaster Toaster;

    public class PropertyQuery
    {
        public Prop Prop;
        public string Property;

        public int Value => Prop.Properties[Property];

        public PropertyQuery(Prop prop, string property)
        {
            Prop = prop;
            Property = property;
        }
    }

    public class PropertyCondition
    {
        public PropertyQuery LeftPropertyQuery, RightPropertyQuery;
        public Query<int> RightValueQuery;
        public Comparison Comparison;

        public int LeftValue => LeftPropertyQuery.Value;

        public int RightValue
        {
            get
            {
                if (RightPropertyQuery != null)
                    return RightPropertyQuery.Value;
                else
                    return RightValueQuery();
            }
        }

        public bool IsTrue => Comparison.Evaluate(LeftValue, RightValue);

        public string Hint
        {
            get
            {
                string hint = LeftPropertyQuery.Prop.ShortName + "'s " +
                              LeftPropertyQuery.Property +
                              " (" + LeftValue + ") must be ";

                switch (Comparison)
                {
                    case Comparison.Equal: hint += "equal to "; break;
                    case Comparison.GreaterThan: hint += "greater than "; break;
                    case Comparison.GreaterThanOrEqual: hint += "greater than or equal to "; break;
                    case Comparison.LessThan: hint += "less than "; break;
                    case Comparison.LessThanOrEqual: hint += "less than or equal to "; break;
                    default: hint += "UNKNOWN COMPARISON "; break;
                }

                if (RightPropertyQuery != null)
                    hint += RightPropertyQuery.Prop.ShortName + "'s " +
                            RightPropertyQuery.Property +
                            " (" + RightValue + ").";
                else
                    hint += RightValue + ".";

                return hint;
            }
        }

        public PropertyCondition(PropertyQuery left, 
                                 Comparison comparison, 
                                 PropertyQuery right)
        {
            LeftPropertyQuery = left;
            RightPropertyQuery = right;

            Comparison = comparison;
        }

        public PropertyCondition(PropertyQuery left,
                                 Comparison comparison,
                                 Query<int> right)
        {
            LeftPropertyQuery = left;
            RightValueQuery = right;

            Comparison = comparison;
        }
    }

    public abstract class Passage : global::Passage
    {
        public Prop Prop;
    }
}


[Serializable]
public class Properties :
    DefaultSerializableDictionary<string, int>
{ }

[Serializable]
public class PropState :
    SerializableDictionary<string, string>
{
    public override bool Equals(object obj)
    {
        if (!(obj is PropState))
            return false;
        PropState other = obj as PropState;

        return other.SetEquality(other);
    }
}

[Serializable]
public class CharacterSprites :
    SerializableDictionary<PropState, Sprite>
{ }


public static class PropExtensions
{
    public static Prop.PropertyCondition IsEqualTo(this Prop.PropertyQuery left, Query<int> right)
        => new Prop.PropertyCondition(left, Comparison.Equal, right);
    public static Prop.PropertyCondition IsEqualTo(this Prop.PropertyQuery left, int right)
        => left.IsEqualTo(() => right); 

    public static Prop.PropertyCondition IsGreaterOrEqualTo(this Prop.PropertyQuery left, Query<int> right)
        => new Prop.PropertyCondition(left, Comparison.GreaterThanOrEqual, right);
    public static Prop.PropertyCondition IsGreaterOrEqualTo(this Prop.PropertyQuery left, int right)
        => left.IsGreaterOrEqualTo(() => right);

    public static Prop.PropertyCondition IsGreaterThan(this Prop.PropertyQuery left, Query<int> right)
        => new Prop.PropertyCondition(left, Comparison.GreaterThan, right);
    public static Prop.PropertyCondition IsGreaterThan(this Prop.PropertyQuery left, int right)
        => left.IsGreaterThan(() => right);

    public static Prop.PropertyCondition IsLessOrEqualTo( this Prop.PropertyQuery left, Query<int> right)
        => new Prop.PropertyCondition(left, Comparison.LessThanOrEqual, right);
    public static Prop.PropertyCondition IsLessOrEqualTo(this Prop.PropertyQuery left, int right)
        => left.IsLessOrEqualTo(() => right);

    public static Prop.PropertyCondition IsLessThan(this Prop.PropertyQuery left, Query<int> right)
        => new Prop.PropertyCondition(left, Comparison.LessThan, right);
    public static Prop.PropertyCondition IsLessThan(this Prop.PropertyQuery left, int right)
        => left.IsLessThan(() => right);
}