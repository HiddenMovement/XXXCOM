using UnityEngine;
using System.Collections;


public class HeadingPassage : Passage
{
    public string Title;

    public static HeadingPassage Make(string title)
    {
        HeadingPassage heading = Make<HeadingPassage>();
        heading.Title = title;

        return heading;
    }
}