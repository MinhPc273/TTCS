using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    public Transform Gun;
    public bool canShoot;

    public Vector3 curPos;
    [SerializeField]private PointSpawn PointSwap;

    public GunData GunData => Gun != null ? Gun.GetComponent<GunData>() : null;

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
                if(this.GunData.Level == PointSwap.GunData.Level)
                {
                    Debug.Log("merge");
                    //this.Gun = null;
                    ObjectPooler.EnqueueObject(this.Gun,GunManager.Instance.PoolParent, "Gun");
                    ObjectPooler.EnqueueObject(PointSwap.Gun, GunManager.Instance.PoolParent, "Gun");

                    int nextLevel = this.GunData.Level + 1;
                    this.Gun = null;

                    PointSwap.Gun = GunManager.Instance.SpawnGunN();
                    PointSwap.Gun.SetParent(PointSwap.transform);
                    PointSwap.Gun.localPosition = Vector3.zero;
                    PointSwap.Gun.gameObject.SetActive(true);
                    PointSwap.Gun.GetComponent<GunData>().setData(nextLevel);
                    
                }
                else
                {
                    this.Gun = PointSwap.Gun;
                    this.Gun.SetParent(this.transform);
                    this.Gun.localPosition = Vector3.zero;

                    PointSwap.Gun = thisGun;
                    PointSwap.Gun.SetParent(PointSwap.transform);
                    PointSwap.Gun.localPosition = Vector3.zero;
                }
            }
            else
            {
                this.Gun = null;

                PointSwap.Gun = thisGun;
                PointSwap.Gun.SetParent(PointSwap.transform);
                PointSwap.Gun.localPosition = Vector3.zero;
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
