using System.Collections;
using UnityEngine;

//****
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