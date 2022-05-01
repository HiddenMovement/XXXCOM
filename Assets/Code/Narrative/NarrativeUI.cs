using System.Collections;
using UnityEngine;

public class NarrativeUI : RectElement
{
    public RectTransform Container;

    public Stage Stage;
    public DialogBox DialogBox;
    public ChoiceUI ChoiceUI;
    public Prop Setting;
    public Toaster Toaster;

    private void Update()
    {
        Container.gameObject.SetActive(The.Narrator.IsReading);
    }
}