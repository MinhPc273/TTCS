using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastScripts : MonoBehaviour
{

    [SerializeField]private Camera Camera;
    public LayerMask _gunMask;
    public LayerMask _placeMask;

    public LayerMask _sellMask;
    private Vector3 mousePosition;
    private RaycastHit hit;

    private bool canMove;
    [SerializeField]private Transform PosSelected;

    private Vector3 startMousePos;
    private Vector3 startGunPos;

    private bool onSell = false;
    private Tween tweenOnSell;

    private Transform sell;

    private void Awake()
    {
        Camera = Camera.main;
        canMove = false;
    }   

    void Update()
    {
        if(Common.IsUI())
        {
            return;
        }  
        this.MoveGun();
    }

    private Transform RayCastPos()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = new Ray(mousePosition, Vector3.forward);
        Debug.DrawRay(mousePosition, Camera.transform.forward, Color.red);
        if (Physics.Raycast(ray, out hit, 30f, _gunMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log(hit.transform.name);
            if(hit.transform.GetComponent<PointSpawn>().Gun == null)
            {
                return null;
            }
            return hit.transform;
        }
        return null;
    }

    private Vector3 RayCastPlane()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = new Ray(mousePosition, Vector3.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _placeMask, QueryTriggerInteraction.Collide))
        {
            //Debug.DrawRay(mousePosition, Vector3.forward * hit.distance, Color.yellow);
            //Debug.Log(hit.point);
            return raycastHit.point;
        }
        return Vector3.zero;
    }

    private Transform RayCastSell()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = new Ray(mousePosition, Vector3.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _sellMask, QueryTriggerInteraction.Collide))
        {
            //Debug.DrawRay(mousePosition, Vector3.forward * hit.distance, Color.yellow);
            //Debug.Log(hit.point);
            return raycastHit.transform;
        }
        return null;
    }

    private void MoveGun()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RayCastPos() != null)
            {
                PosSelected = RayCastPos();
                PosSelected.GetComponent<PointSpawn>().StartSelected();
                canMove = true;
                GUIManager.Instance.OnSell(PosSelected.GetComponent<PointSpawn>().GunData.Level);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            if(PosSelected != null)
            {
                if(sell != null)
                {
                    PosSelected.GetComponent<PointSpawn>().Sell();
                    GUIManager.Instance.Sell();
                }
                else {
                    PosSelected.GetComponent<PointSpawn>().EndSelected();
                }
                GUIManager.Instance.OnSell();
                PosSelected = null;
            }
        }

        if (canMove)
        {
            if(PosSelected != null)
            {
                PosSelected.transform.position = RayCastPlane();
            }
            if(RayCastSell() != null)
            {
                if(onSell) return;
                onSell = true;
                OnSellAnim();
            }
            else {
                if(!onSell) return;
                onSell = false;
                sell.localScale = Vector3.one;
                sell = null;
                tweenOnSell.Kill();
            }
        }
    }

    private void OnSellAnim() 
    {
        sell = RayCastSell();
        sell.localScale = Vector3.one;
        tweenOnSell = sell.transform.DOScale(Vector3.one * 1.1f, 0.6f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }
}
