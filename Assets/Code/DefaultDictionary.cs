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
}


[Serializable]
public class ItemDictionary : DefaultSerializableDictionary<Item, int> { }

[Serializable]
public class AttributeDictionary : DefaultSerializableDictionary<Attribute, int> { }

