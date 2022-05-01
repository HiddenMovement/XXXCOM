using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public partial class Chapter : MonoBehaviour
{
    //***naming clash with NavigationPassage
    public string Title => this.GetType().Name;

    public Book Book => GetComponentInParent<Book>();
    public IEnumerable<Passage> Passages => 
        transform.Children()
        .SelectComponents<Transform, Passage>();

    public Passage FirstPassage => Passages.First();
    public Passage this[string heading_title] =>
        Passages
        .SelectComponents<Passage, HeadingPassage>()
        .FirstOrDefault(heading => heading.Title == heading_title);

    private void Start()
    {
        if (Passages.Count() == 0)
            foreach (Passage passage in Write())
                passage.transform.SetParent(transform);
    }

    private void Update()
    {
        
    }

    public Passage GetFollowingPassage(Passage passage)
    {
        if (!Passages.Contains(passage))
        {
            Passage parent = passage.transform.parent.GetComponentInParent<Passage>();
            if (parent == null)
                return null;

            return GetFollowingPassage(parent);
        }

        return Passages.NextElement(passage);
    }

    protected virtual List<Passage> Write() 
    { return new List<Passage> { Close() }; }
}