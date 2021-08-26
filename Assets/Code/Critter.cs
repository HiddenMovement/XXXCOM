using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;


[RequireComponent(typeof(Resident))]
[RequireComponent(typeof(Obstacle))]
[RequireComponent(typeof(Traits))]
[RequireComponent(typeof(Mortal))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(MoveController))]
public class Critter : MonoBehaviour
{
    public string Name;
    public Squad Squad;

    public GameObject IsSelectedIndicator;

    public Location Location { get { return Resident.Location; } }

    public bool IsSelected
    {
        get { return Selected == this; }

        set
        {
            if (value)
                Selected = this;
            else if (IsSelected)
                Selected = null;
        }
    }

    public Ability SelectedAbility { get; set; }

    public Resident Resident { get { return GetComponent<Resident>(); } }
    public Traits Traits { get { return GetComponent<Traits>(); } }
    public Attributes Attributes { get { return GetComponent<Attributes>(); } }
    public Triggers Triggers { get { return GetComponent<Triggers>(); } }
    public Mortal Mortal { get { return GetComponent<Mortal>(); } }
    public Inventory Inventory { get { return GetComponent<Inventory>(); } }
    public MoveController MoveController { get { return GetComponent<MoveController>(); } }

    public IEnumerable<Ability> Abilities
    { get { return GetComponentsInChildren<Ability>(); } }

    private void Update()
    {
        IsSelectedIndicator.SetActive(IsSelected);
    }

    public static Critter Selected { get; set; }
}

