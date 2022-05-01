using UnityEngine;
using System.Collections;

public class TranslatedPassage : Passage.Script
{
    public Translator Translator;

    public string RawMessage;
    public string Message
    {
        get
        {
            if (Translator == null)
                return RawMessage;

            return Translator.Translate(RawMessage);
        }
    }
}
