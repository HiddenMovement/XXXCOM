using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Translator
{
    public Translator PreviousStep;

    public Dictionary<string, Query<string>> Table =
        new Dictionary<string, Query<string>>();

    public string Translate(string input)
    {
        if (PreviousStep != null)
            input = PreviousStep.Translate(input);

        string output = "";

        foreach (string word in input.Split(' '))
        {
            if (Table.ContainsKey(word))
                output += Table[word]();
            else
                output += word;

            output += " ";
        }

        return output;
    }
}
