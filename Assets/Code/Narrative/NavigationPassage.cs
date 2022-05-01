using UnityEngine;
using System.Collections;

public class NavigationPassage : Passage
{
    public Chapter DesiredChapter;
    public string HeadingTitle;

    public override Passage NextPassage
    {
        get
        {
            Chapter chapter = DesiredChapter;
            if (chapter == null)
                chapter = Chapter;

            if (HeadingTitle == null)
                return chapter.FirstPassage;

            return chapter[HeadingTitle];
        }
    }

    public static NavigationPassage Make(string heading_title = null, Chapter desired_chapter = null)
    {
        NavigationPassage locate_passage = Make<NavigationPassage>();
        locate_passage.DesiredChapter = desired_chapter;
        locate_passage.HeadingTitle = heading_title;

        return locate_passage;
    }
}
