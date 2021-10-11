using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Traits))]
[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Triggers))]
public class Entity : MonoBehaviour
{
    public Traits Traits { get { return GetComponent<Traits>(); } }
    public Attributes Attributes { get { return GetComponent<Attributes>(); } }
    public Triggers Triggers { get { return GetComponent<Triggers>(); } }

    public Body Body { get { return Traits.Body; } }
    public Status Status { get { return Traits.Status; } }


    public bool IsResident { get { return Resident != null; } }
    public Resident Resident { get { return GetComponent<Resident>(); } }
}
