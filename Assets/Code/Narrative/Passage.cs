using UnityEngine;
using System.Collections;

public abstract class Passage : MonoBehaviour
{
    public Chapter Chapter => GetComponentInParent<Chapter>();
    public Book Book => Chapter.Book;

    public virtual Passage NextPassage => Chapter.GetFollowingPassage(this);

    public virtual void Read() { }

    public static T Make<T>() where T : Passage
    { return new GameObject(typeof(T).ToString()).AddComponent<T>(); }


    public class Script : MonoBehaviour
    {
        public Passage Passage => GetComponent<Passage>();
    }
}