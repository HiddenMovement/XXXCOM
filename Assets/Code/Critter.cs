using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Resident))]
[RequireComponent(typeof(Obstacle))]
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

    public Entity Entity { get { return GetComponent<Entity>(); } }
    public Resident Resident { get { return GetComponent<Resident>(); } }
    public MoveController MoveController { get { return GetComponent<MoveController>(); } }

    public IEnumerable<Ability> Abilities
    { get { return GetComponentsInChildren<Ability>(); } }

    private void Update()
    {
        IsSelectedIndicator.SetActive(IsSelected);
    }

    public static Critter Selected { get; set; }
}

