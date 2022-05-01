using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class Audience : MonoBehaviour
{
    SayPassage passage;//****naming
    IEnumerable<string> Words => passage.MessagePassage.Message.Split(' ');
    float AverageWordLength => (float)Words.Average(word => word.Length);

    float elapsed_seconds = 0;

    [Range(0, 10)]
    public float WordsPerSecond = 2.5f;
    public float ExpectedAverageWordLength = 5;
    public float Progress =>
        elapsed_seconds /
        (AverageWordLength / ExpectedAverageWordLength) *
        Words.Count() / WordsPerSecond;

    public bool Manual = true;
    public bool Skip = false;

    public DialogBox DialogBox => GetComponent<DialogBox>();


    private void Update()
    {
        if (Manual)
            return;

        if(passage != DialogBox.SayPassage)
        {
            passage = DialogBox.SayPassage;
            elapsed_seconds = 0;
        }

        elapsed_seconds += Time.deltaTime;
        if (Progress >= 1 || Skip)
            The.DialogBox.Hear();
    }
}
