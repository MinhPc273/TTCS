using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastScripts : MonoBehaviour
{

    [SerializeField]private Camera Camera;
    public LayerMask _gunMask;
    public LayerMask _placeMask;
    private Vector3 mousePosition;
    private RaycastHit hit;

    private bool canMove;
    [SerializeField]private Transform PosSelected;

    private Vector3 startMousePos;
    private Vector3 startGunPos;

    private void Awake()
    {
        Camera = Camera.main;
        canMove = false;
    }   

    void Update()
    {
        if(Common.CheckInputUI())
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

    private void MoveGun()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RayCastPos() != null)
            {
                PosSelected = RayCastPos();
                //GunSelected.GetComponent<GunController>().StartSelected();
                canMove = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            if(PosSelected != null)
            {
                PosSelected.GetComponent<PointSpawn>().EndSelected();
            }
        }

        if (canMove)
        {
            if(PosSelected != null)
            {
                PosSelected.transform.position = RayCastPlane();
            }
        }
    }
}
