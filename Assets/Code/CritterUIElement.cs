using UnityEngine;
using System.Collections;

public class CritterUIElement : UIElement
{
    public Critter Critter { get { return Critter.Selected; } }
}
