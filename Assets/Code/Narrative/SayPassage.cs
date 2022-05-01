using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TranslatedPassage))]
public class SayPassage : Character.Passage
{
    public TranslatedPassage MessagePassage => GetComponent<TranslatedPassage>();

    public bool IsNarrator => Character == null;

    public bool HasBeenHeard = false;

    public override Passage NextPassage
    {
        get
        {
            if (!HasBeenHeard)
                return null;

            return base.NextPassage;
        }
    }

    private void Start()
    {
        if (IsNarrator) ;
        else
            MessagePassage.Translator = Character.Translator;

    }

    public override void Read()
    {
        base.Read();

        The.DialogBox.Say(this);
    }
}


public static class SayPassageExtensions
{
    public static SayPassage Say(this Character character, string dialog)
    {
        SayPassage say = Passage.Make<SayPassage>();
        say.Character = character;
        say.MessagePassage.RawMessage = dialog;

        return say;
    }
}
