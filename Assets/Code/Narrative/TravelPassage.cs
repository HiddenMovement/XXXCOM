using System.Collections;
using UnityEngine;


public class TravelPassage : Passage
{
    public string Destination;

    public override void Read()
    {
        base.Read();

        The.NarrativeUI.Setting.State["Background"] = Destination;
    }

    public static TravelPassage Make(string destination)
    {
        TravelPassage emote = Passage.Make<TravelPassage>();
        emote.Destination = destination;

        return emote;
    }
}