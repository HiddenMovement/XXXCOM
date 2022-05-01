using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public partial class Chapter
{
    protected Stage Stage => The.Stage;

    protected HeadingPassage ___Heading___(string title)
    { return HeadingPassage.Make(title); }

    protected ActionPassage Happen(System.Action action)
    { return ActionPassage.Make(action); }

    protected SayPassage Narrate(string message)
    { return SayPassageExtensions.Say(null, message); }

    protected ActionPassage Close()//***"CloseBook"?
    { return Happen(() => The.Narrator.IsReading = false); }

    protected TravelPassage TravelTo(string destination)
    { return TravelPassage.Make(destination); }

    protected PauseForEffect PauseForEffect(float seconds_to_wait)
    { return global::PauseForEffect.Make(seconds_to_wait); }


    protected IfPassage If(Query<bool> Condition)
    { return IfPassage.Make(Condition, null); }

    protected IfPassage If(Prop.PropertyCondition Condition)
    {
        IfPassage if_passage = IfPassage.Make(() => Condition.IsTrue, null);
        if_passage.Hint = () => Condition.Hint;

        return if_passage;
    }


    protected NavigationPassage Read(string heading_title)
    { 
        return NavigationPassage.Make(heading_title, this); 
    }

    protected NavigationPassage Read<T>(string heading_title = null) where T : Chapter
    {
        Chapter desired_chapter = Book.Chapters
            .FirstOrDefault(chapter_ => chapter_ is T);

        if (desired_chapter == null)
            throw new ArgumentException(
                "Chapter " + typeof(T).ToString() +" not found.");

        return NavigationPassage.Make(heading_title, desired_chapter);
    }
}
