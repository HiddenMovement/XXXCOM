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
    public UI UI;
    public GraphicRaycaster TacticalGraphicRaycaster;
    public PointerWatcher PointerWatcher;
    public Camera TacticalCamera;
    public Map Map;
    public TurnScheduler TurnScheduler;
    public Floor Floor;
    public Transform ExpatContainersContainer;

    public NarrativeUI NarrativeUI;
    public Narrator Narrator;
    public Characters Characters;
    public Stage Stage;
    public DialogBox DialogBox;
    public ChoiceUI ChoiceUI;
}
