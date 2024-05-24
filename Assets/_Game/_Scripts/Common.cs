using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Common
{
    public static bool IsUI()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return true;
            }
        }
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        return false;
    }
}
