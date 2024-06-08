using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScale : Button
{
    //ButtonScaleType scaleType = ButtonScaleType.ZoomIn;

    bool isPressed = false;

    float zoomInScale = 1.1f;

    float zoomSpeed = 0.5f;

    // private enum ButtonScaleType
    // {
    //     ZoomIn,
    //     ZoomOut
    // }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (!interactable) return;
        //AudioManager.Ins.PlayButtonClickSFX();
        isPressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isPressed = false;
    }

    private void LateUpdate()
    {
        Vector3 targetScale = Vector3.one * zoomInScale;
        if (isPressed)
        {
            targetScale = Vector3.one * zoomInScale;
        }
        else
        {
            targetScale = Vector3.one;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, zoomSpeed);
    }
}
