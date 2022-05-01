using UnityEngine;
using System.Collections;


public class ActionPassage : Passage
{
    public System.Action Action;

    public ActionPassage(System.Action action)
    {
        Action = action;
    }

    public override void Read()
    {
        base.Read();

        Action();
    }

    public static ActionPassage Make(System.Action action)
    {
        ActionPassage action_passage = Make<ActionPassage>();
        action_passage.Action = action;

        return action_passage;
    }
}