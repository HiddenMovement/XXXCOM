using UnityEngine;
using System.Collections;


public class RangedAttack : Attack
{
    public float CloseRange, 
          MaximumRange, 
          OutOfRangePenaltyDistance;

    public int PointBlankBonus, 
        CloseRangeBonus, 
        MaximumRangeBonus;

    public override int MiscAttackBonus
    {
        get
        {
            float distance = Caster.Distance(Target);

            if (distance < CloseRange)
            {
                float factor = distance / CloseRange;

                return Mathf.Lerp(
                    PointBlankBonus,
                    CloseRangeBonus,
                    factor).Round();
            }
            else if (distance < MaximumRange)
            {
                float factor = (distance - CloseRange) /
                               (MaximumRange - CloseRange);

                return Mathf.Lerp(
                    CloseRangeBonus,
                    MaximumRangeBonus,
                    factor).Round();
            }
            else
            {
                float penalty = (distance - MaximumRange) /
                                OutOfRangePenaltyDistance;

                return MaximumRangeBonus - 
                       penalty.RoundDown();
            }
        }
    }
}
