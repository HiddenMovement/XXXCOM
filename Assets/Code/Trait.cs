using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class Trait : MonoBehaviour
{
    Dictionary<Delegate, int> responses = 
        new Dictionary<Delegate, int>();

    public AttributeDictionary Bonuses = new AttributeDictionary();

    public string Name;
    public string Description;
    public Sprite Icon;

    public Entity Entity { get { return GetComponentInParent<Entity>(); } }
    public Traits Traits { get { return Entity.Traits; } }
    public Attributes Attributes { get { return Entity.Attributes; } }
    public Triggers Triggers { get { return Entity.Triggers; } }
    public Resident Resident { get { return Traits.GetComponent<Resident>(); } }
    public Critter Critter { get { return Traits.GetComponent<Critter>(); } }

    public Status Status { get { return Traits.Status; } }
    public Body Body { get { return Traits.Body; } }

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
