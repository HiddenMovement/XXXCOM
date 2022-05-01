using UnityEngine;
using System.Collections;

public static class StatePassageExtensions//***move into prop?
{
    public static Prop.StatePassage Pose(this Character character, string pose_)
    {
        Prop.StatePassage pose = Passage.Make<Prop.StatePassage>();
        pose.Prop = character.Prop;
        pose.Key = "Pose";
        pose.Value = pose_;

        return pose;
    }

    public static Prop.StatePassage Emote(this Character character, string emotion)
    {
        Prop.StatePassage emote = Passage.Make<Prop.StatePassage>();
        emote.Prop = character.Prop;
        emote.Key = "Emotion";
        emote.Value = emotion;

        return emote;
    }

    public static Prop.StatePassage Wear(this Character character, string costume)
    {
        Prop.StatePassage wear = Passage.Make<Prop.StatePassage>();
        wear.Prop = character.Prop;
        wear.Key = "Costume";
        wear.Value = costume;

        return wear;
    }
}