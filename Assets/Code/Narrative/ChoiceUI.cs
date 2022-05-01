using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChoiceUI : MonoBehaviour
{
    public ChoicePassage Choice;

    public Transform SpawnPosition, 
                     SolitaryPosition, 
                     BinaryPosition0, BinaryPosition1, 
                     FirstPosition, LastPosition;

    public float AnimationSpeed = 1;

    public OptionButton OptionButtonPrefab;

    public IEnumerable<OptionButton> OptionButtons => 
        GetComponentsInChildren<OptionButton>();

    private void Update()
    {
        int index = 0;
        foreach (OptionButton option_button in OptionButtons)
        {
            Vector3 target_position;

            if (OptionButtons.Count() == 1)
                target_position = SolitaryPosition.position;
            else if (OptionButtons.Count() == 2)
                target_position = index++ == 0 ? BinaryPosition0.position :
                                               BinaryPosition1.position;
            else
            {
                float factor = 
                    index++ / 
                    (float)(Mathf.Max(Choice.Options.Count - 1, 0));

                target_position = Vector3.Lerp(FirstPosition.position,
                                               LastPosition.position,
                                               factor);
            }
      
            if (Choice.ChosenOption != null)
                target_position = SpawnPosition.position;

            option_button.transform.position =
                Vector3.Lerp(option_button.transform.position, 
                             target_position, 
                             AnimationSpeed * 3 * Time.deltaTime);
        }
    }

    //****design
    public void Offer(ChoicePassage choice)
    {
        foreach (OptionButton option_button in OptionButtons)
            Destroy(option_button.gameObject);

        Choice = choice;
        if (Choice == null)
            return;

        foreach(ChoicePassage.Option option in Choice.Options)
        {
            OptionButton option_button = Instantiate(OptionButtonPrefab);
            option_button.Option = option;

            option_button.transform.SetParent(transform);
            option_button.transform.position = 
                SpawnPosition.position;
        }
    }

    public void Rescind()
    {
        Offer(null);
    }
}
