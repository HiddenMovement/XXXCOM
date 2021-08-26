using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public static class InputUtility
{
    public enum MouseButton { Left, Right, Middle }

    public static bool WasMouseLeftPressed { get { return Input.GetMouseButtonDown((int)MouseButton.Left); } }
    public static bool IsMouseLeftPressed { get { return Input.GetMouseButton((int)MouseButton.Left); } }
    public static bool WasMouseLeftReleased { get { return Input.GetMouseButtonUp((int)MouseButton.Left); } }

    public static bool WasMouseRightPressed { get { return Input.GetMouseButtonDown((int)MouseButton.Right); } }
    public static bool IsMouseRightPressed { get { return Input.GetMouseButton((int)MouseButton.Right); } }
    public static bool WasMouseRightReleased { get { return Input.GetMouseButtonUp((int)MouseButton.Right); } }

    public static Vector2 GetMouseMotion()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    
    //This should only be called from OnGUI()
    public static bool ConsumeIsKeyUp(KeyCode key_code)
    {
        if (Event.current.isKey && Event.current.keyCode == key_code && Input.GetKeyUp(key_code))
        {
            Event.current.Use();

            return true;
        }

        return false;
    }
}
