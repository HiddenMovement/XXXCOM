using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Traits))]
public class Mortal : MonoBehaviour
{
    public int MaxHealth
    { get { return Traits.Attributes[Attribute.Health]; } }

    public int CurrentHealth
    { get { return MaxHealth - Traits.Attributes[Attribute.DamageTaken]; } }

    public Traits Traits { get { return GetComponent<Traits>(); } }

    private void Update()
    {
        if (CurrentHealth <= 0)
            Traits.Triggers.OnDie();
    }
}


