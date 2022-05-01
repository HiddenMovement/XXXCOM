using UnityEngine;
using System.Collections;


public class PosePassage : Prop.StatePassage
{
    public string Pose;

    public override void Read()
    {
        base.Read();

        Prop.State["Pose"] = Pose;//****consider a ActorPassage with aliases for Clothing, Pose, Emotion
    }
}

public static class PosePassageExtensions
{
    public static PosePassage Pose(this Character character, string pose_)
    {
        PosePassage pose = Passage.Make<PosePassage>();
        pose.Prop = character.Prop;
        pose.Pose = pose_;

        return pose;
    }
}