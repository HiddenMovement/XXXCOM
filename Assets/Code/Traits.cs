using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Triggers))]
public class Traits : MonoBehaviour, IEnumerable<Trait>
{
    public Transform TraitsContainer;

    public IEnumerable<Ability> Abilities
    { get { return this.SelectComponents<Trait, Ability>(); } }

    public Body Body { get { return Demand<Body>(); } }
    public Status Status { get { return Demand<Status>(); } }

    public void AddTrait<T>(T trait) where T : Trait
    {
        trait.transform.SetParent(TraitsContainer);
    }

    public Trait RemoveTrait(Trait trait)
    {
        trait.transform.SetParent(null);

        return trait;
    }

    public T Get<T>() where T : Trait
    {
        return TraitsContainer.GetComponentInChildren<T>();
    }

    public T Demand<T>() where T : Trait
    {
        T trait = Get<T>();
        if (trait == null)
        {
            trait = new GameObject(typeof(T).Name).AddComponent<T>();
            trait.transform.SetParent(TraitsContainer);
        }

        return trait;
    }

    public IEnumerator<Trait> GetEnumerator()
    {
        IEnumerable<Trait> traits = GetComponentsInChildren<Trait>();

        return traits.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
