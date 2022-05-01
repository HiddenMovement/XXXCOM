using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

//***Replace with => notation
public static class The
{
    //TheShortcuts.py (Don't modify this comment)

    public static Style Style { get { return Directory.Style; } }
    public static Canvas TacticalCanvas { get { return Directory.TacticalCanvas; } }//***rename to just Canvas
    public static UI UI { get { return Directory.UI;} }
    public static GraphicRaycaster TacticalGraphicRaycaster { get { return Directory.TacticalGraphicRaycaster; } }
    public static PointerWatcher PointerWatcher { get { return Directory.PointerWatcher; } }
    public static Camera TacticalCamera { get { return Directory.TacticalCamera; } }
    public static Map Map { get { return Directory.Map; } }
    public static TurnScheduler TurnScheduler { get { return Directory.TurnScheduler; } }
    public static Floor Floor { get { return Directory.Floor; } }
    public static Transform ExpatContainersContainer { get { return Directory.ExpatContainersContainer; } }

    public static NarrativeUI NarrativeUI{ get { return Directory.NarrativeUI; } }
    public static Narrator Narrator { get { return Directory.Narrator; } }
    public static Characters Characters { get { return Directory.Characters; } }
    public static Stage Stage { get { return Directory.Stage; } }
    public static DialogBox DialogBox { get { return Directory.DialogBox; } }
    public static ChoiceUI ChoiceUI { get { return Directory.ChoiceUI; } }


    static Directory directory;
    public static Directory Directory
    {
        get
        {
            if (directory == null)
                directory = GameObject.FindObjectOfType<Directory>();

            return directory;
        }
    }
}
