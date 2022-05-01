using UnityEngine;
using System.Collections;

public class NavigationPassage : Passage
{
    public Chapter DesiredChapter;
    public string Title;

    public override Passage NextPassage
    {
        get
        {
            Chapter chapter = DesiredChapter;
            if (chapter == null)
                chapter = Chapter;

            if (Title == null)
                return chapter.StartPassage;

            return chapter[Title];
        }
    }

    public static NavigationPassage Make(string title = null, Chapter desired_chapter = null)
    {
        NavigationPassage locate_passage = Make<NavigationPassage>();
        locate_passage.DesiredChapter = desired_chapter;
        locate_passage.Title = title;

        return locate_passage;
    }
}
