using UnityEngine;
using System.Collections;

[System.Serializable]
public struct TranslatorString
{
    public string RawString;
    public string TranslatedString
    {
        get
        {
            if (Translator == null)
                return RawString;

            return Translator.Translate(RawString);
        }
    }

    public Translator Translator;


    public TranslatorString(string raw_string, Translator translator = null)
    {
        RawString = raw_string;
        Translator = translator;
    }
}
