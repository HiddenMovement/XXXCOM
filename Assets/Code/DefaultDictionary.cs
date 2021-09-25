using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class DefaultSerializableDictionary<T, U> 
    : SerializableDictionary<T, U>
{
    new public U this[T t]
    {
        get
        {
            if (!ContainsKey(t))
                this[t] = default;

            return base[t];
        }

        set
        {
            base[t] = value;
        }
    }

    public void Load(DefaultSerializableDictionary<T, U> other)
    {
        foreach (T key in other.Keys)
            this[key] = other[key];
    }
}

[Serializable]
public class AttributeDictionary : DefaultSerializableDictionary<Attribute, int>
{
    public void Add(AttributeDictionary other)
    {
        foreach (Attribute attribute in other.Keys)
            this[attribute] += other[attribute];
    }

    public void Subtract(AttributeDictionary other)
    {
        foreach (Attribute attribute in other.Keys)
            this[attribute] -= other[attribute];
    }
}

