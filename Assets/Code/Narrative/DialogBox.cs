using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//****naming
//****Need way to hide namebox when narrating. Also dynamic dialogbox height? Also fix shadow
public class DialogBox : UIElement
{
    public SayPassage SayPassage;//****

    public Text NameText, DialogText;

    public List<Image> Color0Images,
                       Color1Images;//****naming

    //****"DialogContainer" naming
    public RectTransform DialogContainer,
                         NameBox,
                         QuotationMarksContainer;//****NameBox script?
                         

    private void Update()
    {
        if (SayPassage == null)
        {
            DialogContainer.gameObject.SetActive(false);
            return;
        }
        else
            DialogContainer.gameObject.SetActive(true);

        Color color0 = Color.black;
        if(SayPassage.Character != null)
            color0 = SayPassage.Character.Prop.PrimaryColor;
        Color color1 = color0.Lerped(Color.white, 0.5f);

        foreach (Image image in Color0Images)
            image.color = color0;
        foreach (Image image in Color1Images)
            image.color = color1;

        QuotationMarksContainer.gameObject
            .SetActive(SayPassage.Character != null);

        //****Way to function just using a public MessagePAssage? Also ChoiceUI

        if (WasClicked)
            Hear();
    }

    public void Say(SayPassage narrative_passage)
    {
        SayPassage = narrative_passage;

        if (SayPassage.Character != null)
            NameText.text = SayPassage.Character.Name;
        else
            NameText.text = "Narrator";

        DialogText.text = SayPassage.MessagePassage.Message;
    }

    //****See/Hear/Read... See seems the worst of the three. But "Read" is already taken and "Hear" is weird because its not actually spoken
    public void Hear()
    {
        SayPassage.HasBeenHeard = true;
    }
}
