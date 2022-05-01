using UnityEngine;
using System.Collections;


public class EmotePassage : Prop.StatePassage
{
    public string Emotion;

    public override void Read()
    {
        base.Read();

        Prop.State["Emotion"] = Emotion;
    }
}

public static class EmotePassageExtensions
{
    public static EmotePassage Emote(this Character character, string emotion)
    {
        EmotePassage emote = Passage.Make<EmotePassage>();
        emote.Prop =  character.Prop;
        emote.Emotion = emotion;

        return emote;
    }
}