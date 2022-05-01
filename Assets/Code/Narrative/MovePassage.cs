using UnityEngine;
using System.Collections;

//****maybe this is a generic animation Passage, and then we add "StandHerePassage"?
public class MovePassage : Character.Passage
{
    public StagePosition Origin, Destination;

    public override Passage NextPassage
    {
        get
        {
            float distance = 
                Character.transform.position
                .Distance(Destination.transform.position);

            if (distance > 200)
                return null;

            return base.NextPassage;
        }
    }

    public override void Read()
    {
        base.Read();

        if (Origin != null)
            Character.transform.position = Origin.transform.position;

        Character.transform.SetParent(Destination.transform);
    }
}


public static class MovePassageExtensions
{
    public static MovePassage Move(
        this Character character,
        StagePosition destination,
        StagePosition origin = null)
    {
        MovePassage move = Passage.Make<MovePassage>();
        move.Character = character;
        move.Origin = origin;
        move.Destination = destination;

        return move;
    }

    public static MovePassage EnterFrom(this Character character, 
                                        StagePosition origin)
    {
        return character.Move(null, origin);
    }

    public static MovePassage AndStandAt(this MovePassage move_passage, 
                                         StagePosition destination)
    {
        move_passage.Destination = destination;

        return move_passage;
    }

    public static MovePassage ExitTo(this Character character, 
                                     StagePosition destination)
    {
        return character.Move(destination);
    }

}