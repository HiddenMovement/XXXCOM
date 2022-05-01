using UnityEngine;
using System.Collections;


public class SayPassage : Character.Passage
{
    public TranslatorString Message;

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
            Message.Translator = Character.Translator;

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
        say.Message.RawString = dialog;

        return say;
    }
}
