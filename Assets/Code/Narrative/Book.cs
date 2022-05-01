using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Book : MonoBehaviour
{
    Passage current_passage;

    public IEnumerable<Chapter> Chapters => GetComponentsInChildren<Chapter>();
    public Chapter FirstChapter => Chapters.First();

    public IEnumerable<Passage> Passages =>
        Chapters.SelectMany(chapter => chapter.Passages);

    public void Read()
    {
        if (current_passage == null)
        {
            current_passage = FirstChapter.FirstPassage;

            if(current_passage == null)
                return;
        }
        
        if(current_passage.NextPassage == null)
            return;

        current_passage = current_passage.NextPassage;
        current_passage.Read();
    }
}