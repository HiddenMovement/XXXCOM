using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;


public static class The
{
    //TheShortcuts.py (Don't modify this comment)

    public static Style Style => Directory.Style;
    public static Canvas Canvas => Directory.Canvas;
    public static UI UI => Directory.UI;
    public static GraphicRaycaster TacticalGraphicRaycaster => Directory.TacticalGraphicRaycaster;
    public static PointerWatcher PointerWatcher => Directory.PointerWatcher;
    public static Camera TacticalCamera => Directory.TacticalCamera;
    public static Map Map => Directory.Map;
    public static TurnScheduler TurnScheduler => Directory.TurnScheduler;
    public static Floor Floor => Directory.Floor;
    public static Transform ExpatContainersContainer => Directory.ExpatContainersContainer;

    public static NarrativeUI NarrativeUI => Directory.NarrativeUI;
    public static Narrator Narrator => Directory.Narrator;
    public static Characters Characters => Directory.Characters;
    public static Stage Stage => Directory.Stage;
    public static DialogBox DialogBox => Directory.DialogBox;
    public static ChoiceUI ChoiceUI => Directory.ChoiceUI;

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
