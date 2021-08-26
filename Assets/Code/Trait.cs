using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class Trait : MonoBehaviour
{
    Dictionary<Delegate, int> responses = 
        new Dictionary<Delegate, int>();

    public AttributeDictionary AttributesBonuses = new AttributeDictionary();

    public string Name;
    public string Description;
    public Sprite Icon;

    public Traits Traits { get { return GetComponentInParent<Traits>(); } }
    public Attributes Attributes { get { return GetComponentInParent<Attributes>(); } }
    public Triggers Triggers { get { return Traits.GetComponent<Triggers>(); } }
    public Resident Resident { get { return Traits.GetComponent<Resident>(); } }
    public Critter Critter { get { return Traits.GetComponent<Critter>(); } }
    public Inventory Inventory { get { return Traits.GetComponent<Inventory>(); } }

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void OnDestroy() { }

    protected void AddResponse<T>(T response, int priority = 0) 
        where T : Delegate
    {
        responses[response] = priority;
    }

    public IEnumerable<T> GetResponses<T>() where T : Delegate
    {
        return responses.Keys
            .Where(response => response is T)
            .Select(response => response as T);
    }

    public int GetResponsePriority(Delegate response)
    {
        return responses[response];
    }
}
