using UnityEngine;
using System.Collections;


public abstract class Ability : Trait
{
    public int BaseCost;
    public virtual int CostMultiplier { get { return 1; } }
    public int Cost { get { return BaseCost * CostMultiplier; } }

    public virtual bool CanDo
    { get { return Cost <= Inventory[Item.ActionPoints]; } }

    public bool IsSelected
    {
        get { return Critter.SelectedAbility == this; }
        set
        {
            if (value)
                Critter.SelectedAbility = this;
            else if (IsSelected)
                Critter.SelectedAbility = null;
        }
    }

    public virtual void Do()
    {
        Inventory[Item.ActionPoints] -= Cost;
    }

    public bool TryDo()
    {
        if (!CanDo)
            return false;

        Do();
        return true;
    }
}
