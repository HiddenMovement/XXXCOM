using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public static class The
{
    //TheShortcuts.py (Don't modify this comment)

    public static Style Style { get { return Directory.Style; } }
    public static Canvas TacticalCanvas { get { return Directory.TacticalCanvas; } }
    public static GraphicRaycaster TacticalGraphicRaycaster { get { return Directory.TacticalGraphicRaycaster; } }
    public static PointerWatcher PointerWatcher { get { return Directory.PointerWatcher; } }
    public static Camera TacticalCamera { get { return Directory.TacticalCamera; } }
    public static Map Map { get { return Directory.Map; } }
    public static TurnScheduler TurnScheduler { get { return Directory.TurnScheduler; } }
    public static Floor Floor { get { return Directory.Floor; } }
    public static Transform ExpatContainersContainer { get { return Directory.ExpatContainersContainer; } }


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
