using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Directory : MonoBehaviour
{
    public Style Style;
    public Canvas TacticalCanvas;
    public GraphicRaycaster TacticalGraphicRaycaster;
    public PointerWatcher PointerWatcher;
    public Camera TacticalCamera;
    public Map Map;
    public TurnScheduler TurnScheduler;
    public Floor Floor;
    public MapCursor MapCursor;
    public Transform ExpatContainersContainer;
}
