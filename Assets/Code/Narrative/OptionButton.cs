using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OptionButton : MonoBehaviour
{
    public ChoicePassage.Option Option;

    public Text Text, Hint;

    public Button Button => GetComponent<Button>();
    public ChoiceUI ChoiceUI => GetComponentInParent<ChoiceUI>();

    private void Start()
    {
        Color color = ChoiceUI.Choice.Character.Prop.PrimaryColor;
        Button.Image.color = color;
        Button.TouchColor = color.Lerped(Color.white, 0.1f);
        Button.DownColor = color.Lerped(Color.black, 0.1f);

        Text.text = Option.Message.TranslatedString;
        if(Option.Condition != null)
            Text.color = Option.Condition() ? Color.green :
                                              Color.red;
        Hint.text = Option.Hint();

        Button.OnButtonUp.AddListener(() =>
        {
            if(ChoiceUI.Choice.ChosenOption == null)
                ChoiceUI.Choice.ChosenOption = Option;
        });
    }

    private void Update()
    {
        if (Option.Condition != null)
            Button.IsSelectable = Option.Condition();
    }
}