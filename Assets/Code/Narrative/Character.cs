using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;


[RequireComponent(typeof(Prop))]
public class Character : MonoBehaviour
{
    public AudioSource Mouth;

    public Translator Translator = new Translator();

    public Prop Prop => GetComponent<Prop>();
    public string Name => Prop.Name;
    public string Nickname => Prop.ShortName;
    public Prop.PropertyQuery this[string property] => Prop[property];


    public abstract class Passage : global::Passage
    {
        public Character Character;
    }
}


public static class CharacterExtensions
{
    public static ChoicePassage Choose(this Character character,
                                       ChoicePassage.Option option0,
                                       ChoicePassage.Option option1)
    {
        return ChoicePassage.Make(
            character,
            Utility.List(option0, option1));
    }

    public static ChoicePassage Choose(this Character character,
                                       ChoicePassage.Option option0,
                                       ChoicePassage.Option option1,
                                       ChoicePassage.Option option2)
    {
        return ChoicePassage.Make(
            character,
            Utility.List(option0, option1, option2));
    }

    public static ChoicePassage Choose(this Character character,
                                       ChoicePassage.Option option0,
                                       ChoicePassage.Option option1,
                                       ChoicePassage.Option option2,
                                       ChoicePassage.Option option3)
    {
        return ChoicePassage.Make(
            character,
            Utility.List(option0, option1, option2, option3));
    }

    public static ChoicePassage Choose(this Character character,
                                       ChoicePassage.Option option0,
                                       ChoicePassage.Option option1,
                                       ChoicePassage.Option option2,
                                       ChoicePassage.Option option3,
                                       ChoicePassage.Option option4)
    {
        return ChoicePassage.Make(
            character,
            Utility.List(option0, option1, option2, option3, option4));
    }
}


