using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public bool RequiresLineOfSight = true;
    public bool HasLineOfSight => 
        !Caster.IsResident || !Target.IsResident ||
        Caster.Resident.HasLineOfSight(Target.Resident);

    public abstract float SuccessChance { get; }

    public Entity Caster { get { return SpellAbility.Entity; } }
    public Entity Target { get { return SpellAbility.Target; } }

    public SpellAbility SpellAbility
    { get { return GetComponentInParent<SpellAbility>(); } }

    public void Cast()
    {
        if (MathUtility.Flip(SuccessChance))
            Succeed();
        else
            Fail();
    }

    protected virtual void Succeed() { }
    protected virtual void Fail() { }
}