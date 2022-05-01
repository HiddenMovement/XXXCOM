using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;


[RequireComponent(typeof(Prop))]
public class Character : MonoBehaviour
{
    public AudioSource Mouth;

    public Translator Translator = new Translator();

    public Prop Prop => GetComponent<Prop>();
    public string Name => Prop.Name;
    public string Nickname => Prop.ShortName;
    public Prop.PropertyQuery this[string property] => Prop[property];


    public abstract class Passage : global::Passage
    {
        public Character Character;
    }
}




