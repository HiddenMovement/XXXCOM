using UnityEngine;
using System.Collections;


public class WearPassage : Prop.StatePassage
{
    public string Costume;

    public override void Read()
    {
        base.Read();

        Prop.State["Costume"] = Costume;
    }
}

public static class WearPassageExtensions
{
    public static WearPassage Wear(this Character character, string costume)
    {
        WearPassage wear = Passage.Make<WearPassage>();
        wear.Prop = character.Prop;
        wear.Costume = costume;

        return wear;
    }
}