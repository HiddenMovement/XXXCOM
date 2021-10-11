using UnityEngine;
using System.Collections;


public class SpellAbility : TargetEntityAbility
{
    Spell Spell { get { return GetComponentInChildren<Spell>(); } }

    public override bool CanDo
    {
        get
        {
            if (!base.CanDo)
                return false;

            return Spell.SuccessChance > 0;
        }
    }

    public override void Do()
    {
        base.Do();

        Spell.Cast();
    }
}