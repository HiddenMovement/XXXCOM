using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Storage for abstract and real items. 

public class Inventory : MonoBehaviour
{
    public ItemDictionary items = new ItemDictionary();

    [System.Runtime.CompilerServices.IndexerName("Items")]
    public int this[Item item]
    {
        get { return items[item]; }
        set { items[item] = value; }
    }
}

public enum Item { ActionPoints, ReactionPoints };


