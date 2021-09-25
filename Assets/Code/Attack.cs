using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Attack : Spell
{
    public List<Attribute> AttackBonuses,
                           DefenseBonuses,
                           Maladies;
    public int Difficulty = 0;

    public HitChanceFormula Formula;

    public List<DamageParameters> HitDamageParameterss, 
                            MissDamageParameterss;

    public Transform HitContainer, MissContainer;


    public virtual int MiscAttackBonus => 0;

    public int AttackScore
    {
        get
        {
            int attack_bonus = 
                AttackBonuses.Sum(attribute => Caster.Attributes[attribute]) + 
                MiscAttackBonus;

            if (Difficulty < 0)
                attack_bonus += -Difficulty;

            attack_bonus += Maladies
                .Where(attribute => Target.Attributes[attribute] > 0)
                .Sum(attribute => Target.Attributes[attribute]);

            return attack_bonus;
        }
    }

    public int DefenseScore
    {
        get
        {
            int defense_bonus = DefenseBonuses.Sum(attribute => Caster.Attributes[attribute]);

            if (Difficulty > 0)
                defense_bonus += Difficulty;

            defense_bonus += Maladies
                .Where(attribute => Target.Attributes[attribute] < 0)
                .Sum(attribute => -Target.Attributes[attribute]);

            return defense_bonus;
        }
    }

    public override float SuccessChance
    {
        get
        {
            if (RequiresLineOfSight && !HasLineOfSight)
                return 0;

            int attack_score = AttackScore,
                defense_score = DefenseScore;

            switch (Formula)
            {
                case HitChanceFormula.DifferenceOverConstant:
                    return (attack_score - defense_score) / 10;

                case HitChanceFormula.FractionOfTotal:
                    return attack_score / (defense_score + attack_score);

                case HitChanceFormula.FractionOfPowerTotal:
                    int power = 3;
                    attack_score = MathUtility.Pow(attack_score, power);
                    defense_score = MathUtility.Pow(defense_score, power);

                    return attack_score / (defense_score + attack_score);

                case HitChanceFormula.AttackOverDefense:
                    return attack_score / defense_score;

                case HitChanceFormula.D20:
                    return (attack_score - defense_score + 1) / 20;

                default: return 0;
            }
        }
    }

    protected override void Succeed()
    {
        base.Succeed();

        Target.Status.Lose(CalculateDamage(HitDamageParameterss));

        Wound wound = GenerateHitWound();
        if (wound != null)
            Target.Body.Injure(wound);
    }

    protected override void Fail()
    {
        base.Fail();

        Target.Status.Lose(CalculateDamage(MissDamageParameterss));

        Wound wound = GenerateMissWound();
        if (wound != null)
            Target.Body.Injure(wound);
    }

    public AttributeDictionary CalculateDamage(List<DamageParameters> damage_parameterss)
    {
        AttributeDictionary damage = new AttributeDictionary();

        foreach (DamageParameters element in damage_parameterss)
        {
            if (element.DamagedAttribute == Attribute.None)
                continue;

            int sign = (element.BaseDamage >= 0 ? 1 : -1) * 
                       (element.IsMalady ? -1 : 1);

            int bonus_damage = 0;
            if (element.BonusDamage != Attribute.None)
            {
                int attribute_value = Caster.Attributes[element.BonusDamage];
                bonus_damage = (attribute_value / 2.0f).RoundDown();
            }

            damage[element.DamagedAttribute] =
                element.BaseDamage +
                sign * MathUtility.Roll(element.DamageRange) +
                sign * bonus_damage;
        }

        return damage;
    }

    Wound GenerateWound(IEnumerable<Wound> wounds)
    {
        return Instantiate(wounds.ChooseComponentAtRandom());
    }

    Wound GenerateHitWound()
    {
        IEnumerable<Wound> wounds =
            HitContainer.GetComponentsInChildren<Wound>();

        return GenerateWound(wounds);
    }

    Wound GenerateMissWound()
    {
        IEnumerable<Wound> wounds =
            MissContainer.GetComponentsInChildren<Wound>();

        return GenerateWound(wounds);
    }


    public enum HitChanceFormula
    {
        DifferenceOverConstant,
        FractionOfTotal,
        FractionOfPowerTotal,
        AttackOverDefense,
        D20
    }

    [Serializable]
    public struct DamageParameters
    {
        public Attribute DamagedAttribute;
        public bool IsMalady;

        public int BaseDamage, DamageRange;
        public Attribute BonusDamage;
    }
}
