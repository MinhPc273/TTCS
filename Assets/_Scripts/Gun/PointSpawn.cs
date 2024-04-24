using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    public Transform Gun;
    public bool canShoot;

    public Vector3 curPos;
    [SerializeField]private PointSpawn PointSwap;

    private void Start()
    {
        curPos = transform.position;
    }

    public void EndSelected()
    {
        if(PointSwap != null)
        {
            //this.GunSetParent(PointSwap);
            Transform thisGun = this.Gun;
            if(PointSwap.Gun != null)
            {
                this.Gun = PointSwap.Gun;
                this.Gun.position = this.curPos;
                this.Gun.SetParent(this.transform);

                PointSwap.Gun = thisGun;
                PointSwap.Gun.position = PointSwap.curPos;
                PointSwap.Gun.SetParent(PointSwap.transform);
            }
            else
            {
                this.Gun = null;

                PointSwap.Gun = thisGun;
                if(PointSwap.Gun != null)
                {
                    PointSwap.Gun.position = PointSwap.curPos;
                    PointSwap.Gun.SetParent(PointSwap.transform);
                }
            }
        }
        this.transform.position = curPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PosGun")
        {
            PointSwap = other.GetComponent<PointSpawn>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PosGun")
        {
            PointSwap = null;
        }
    }
}
