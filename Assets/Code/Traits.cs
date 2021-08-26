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

    public Attributes Attributes { get { return GetComponent<Attributes>(); } }
    public Triggers Triggers { get { return GetComponent<Triggers>(); } }

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
