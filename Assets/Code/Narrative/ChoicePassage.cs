using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ChoicePassage : Character.Passage
{
    public List<Option> Options = new List<Option>();
    public Option ChosenOption = null;

    public override Passage NextPassage
    {
        get
        {
            if (ChosenOption == null)
                return null;

            if (ChosenOption.Consequence == null)
                return base.NextPassage;

            return ChosenOption.Consequence.NextPassage;
        }
    }

    public override void Read()
    {
        base.Read();

        The.ChoiceUI.Offer(this);
    }

    public static ChoicePassage Make(Character character, List<Option> options)
    {
        ChoicePassage choice = Make<ChoicePassage>();
        choice.Character = character;
        choice.Options = options;

        foreach (Option option in options)
            if(option.Consequence != null)
                option.Consequence.transform.SetParent(choice.transform);

        return choice;
    }

    [Serializable]
    public class Option
    {
        public TranslatorString Message;
        public Query<string> Hint;
        public Passage Consequence;
        public Query<bool> Condition;
    }
}


public static class Suggest
{
    public static ChoicePassage.Option Option(string message)
    {
        return new ChoicePassage.Option
        {
            Message = new TranslatorString(message),
            Hint = () => ""
        };
    }

    public static ChoicePassage.Option WithConsequence(
        this ChoicePassage.Option option,
        Passage consequence)
    {
        if (option.Consequence != null)
            GameObject.Destroy(option.Consequence.gameObject);

        return new ChoicePassage.Option
        {
            Message = option.Message,
            Hint = option.Hint,
            Consequence = consequence,
            Condition = option.Condition
        };
    }

    public static ChoicePassage.Option If(
        this ChoicePassage.Option option,
        Query<bool> Condition)
    {
        return new ChoicePassage.Option
        {
            Message = option.Message,
            Hint = option.Hint,
            Consequence = option.Consequence,
            Condition = Condition
        };
    }

    public static ChoicePassage.Option If(
        this ChoicePassage.Option option,
        Prop.PropertyCondition condition)
    {
        option = option.If(() => condition.IsTrue);

        option.Hint = () => condition.Hint;

        return option;
    }

    public static ChoicePassage.Option WithHint(
        this ChoicePassage.Option option,
        string hint)
    {
        return new ChoicePassage.Option
        {
            Message = option.Message,
            Hint = () => hint,
            Consequence = option.Consequence,
            Condition = option.Condition
        };
    }
}