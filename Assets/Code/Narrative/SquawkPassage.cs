using UnityEngine;
using System.Collections;


public class SquawkPassage : Character.Passage
{
    public AudioClip Sound;

    public override void Read()
    {
        base.Read();

        Character.Mouth.PlayOneShot(Sound);
    }

    public static SquawkPassage Make(Character character, AudioClip sound)
    {
        SquawkPassage squawk = Make<SquawkPassage>();
        squawk.Character = character;
        squawk.Sound = sound;

        return squawk;
    }
}