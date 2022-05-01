using System.Collections;
using UnityEngine;

//****consider making this The.NarrativeUI (And getting rid of other singletons)
//****naming
public class NarrativeUI : RectElement
{
    public RectTransform Container;

    public Stage Stage;
    public DialogBox DialogBox;
    public ChoiceUI ChoiceUI;
    public Prop Setting;//***
    public Toaster IfToaster;//****More generic name?

    private void Update()
    {
        Container.gameObject.SetActive(The.Narrator.IsReading);
    }
}