using UnityEngine;
using System.Collections;


public class IfPassage : Passage
{
    public Query<bool> Condition;
    public Passage TrueConsequence, FalseConsequence;
    public Query<string> Hint;

    public override Passage NextPassage
    {
        get
        {
            if (Condition())
                return TrueConsequence;
            else if (FalseConsequence != null)
                return FalseConsequence;

            return base.NextPassage;
        }
    }

    public override void Read()
    {
        base.Read();

        Color toast_color = Color.green;
        if (Condition())
            TrueConsequence.Read();
        else
            toast_color = Color.red;

        The.NarrativeUI.IfToaster.MakeToast(Hint(), toast_color);
    }

    public static IfPassage Make(Query<bool> Condition, 
                                 Passage true_consequence = null, 
                                 Passage false_consequence = null)
    {
        IfPassage if_passage = Make<IfPassage>();
        if_passage.Condition = Condition;
        if_passage.TrueConsequence = true_consequence;
        if_passage.FalseConsequence = false_consequence;

        if (true_consequence != null)
            true_consequence.transform.SetParent(if_passage.transform);
        if (false_consequence != null)
            false_consequence.transform.SetParent(if_passage.transform);

        return if_passage;
    }
}


public static class IfPassageExtensions
{
    public static IfPassage Then(this IfPassage if_passage,
                                 Passage consequence)
    {
        if_passage.TrueConsequence = consequence;

        //***Not polymorphic, also this doesn't get set if the Otherwise is a navigation passage
        if (consequence is NavigationPassage)
        {
            NavigationPassage navigation_passage = 
                consequence as NavigationPassage;

            Query<string> current_hint_query = if_passage.Hint;

            if_passage.Hint = () =>
            {
                string hint = current_hint_query();

                NavigationPassage true_navigation_passage = 
                    consequence as NavigationPassage;
                NavigationPassage false_navigation_passage =
                    if_passage.FalseConsequence as NavigationPassage;

                NavigationPassage visit_passage = null,
                                  miss_passage = null;

                if (if_passage.Condition())
                {
                    if (true_navigation_passage != null)
                        visit_passage = true_navigation_passage;
                    if (false_navigation_passage != null)
                        miss_passage = false_navigation_passage;
                }
                else
                {
                    if (false_navigation_passage != null)
                        visit_passage = false_navigation_passage;
                    if (true_navigation_passage != null)
                        miss_passage = true_navigation_passage;
                }

                if (visit_passage != null)
                {
                    //***naming
                    string title = visit_passage.Title;
                    if (title == null)
                        title = visit_passage.DesiredChapter.Title;

                    hint += " Going to \"" + title + "\". ";
                }
                if (miss_passage != null)
                {
                    string title = miss_passage.Title;
                    if (title == null)
                        title = miss_passage.DesiredChapter.Title;

                    hint += " Missed \"" + title + "\".";
                }

                return hint;
            };
        }

        if (consequence != null)
            consequence.transform.SetParent(if_passage.transform);

        return if_passage;
    }

    //***Just make users use a "Do"?
    //***Where to store nested Passages? How to? Should we?
    public static IfPassage Then(this IfPassage if_passage,
                                 System.Action consequence)
    {
        return if_passage.Then(ActionPassage.Make(consequence));
    }

    public static IfPassage Otherwise(this IfPassage if_passage,
                                      Passage consequence)
    {
        if_passage.FalseConsequence = consequence;

        if (consequence != null)
            consequence.transform.SetParent(if_passage.transform);

        return if_passage;
    }

    public static IfPassage Otherwise(this IfPassage if_passage,
                                      System.Action consequence)
    {
        return if_passage.Otherwise(ActionPassage.Make(consequence));
    }
}