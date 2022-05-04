
public class SetStatePassage : Prop.Passage
{
    public string Key, Value;

    public SetStatePassage(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public override void Read()
    {
        base.Read();

        Prop.State[Key] = Value;
    }

    public override global::Passage NextPassage
    {
        get
        {
            if (Prop.HasComponent<PropVisualization>() &&
                Prop.GetComponent<PropVisualization>().IsTransitioning)
                return null;

            return base.NextPassage;
        }
    }
}

public static class SetStatePassageExtensions
{
    public static SetStatePassage Pose(this Character character, string pose_)
    {
        SetStatePassage pose = Passage.Make<SetStatePassage>();
        pose.Prop = character.Prop;
        pose.Key = "Pose";
        pose.Value = pose_;

        return pose;
    }

    public static SetStatePassage Emote(this Character character, string emotion)
    {
        SetStatePassage emote = Passage.Make<SetStatePassage>();
        emote.Prop = character.Prop;
        emote.Key = "Emotion";
        emote.Value = emotion;

        return emote;
    }

    public static SetStatePassage Wear(this Character character, string costume)
    {
        SetStatePassage wear = Passage.Make<SetStatePassage>();
        wear.Prop = character.Prop;
        wear.Key = "Costume";
        wear.Value = costume;

        return wear;
    }
}