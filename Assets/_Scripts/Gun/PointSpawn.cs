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

    private PointData pointData = new PointData();
    private string key;

    private void Awake()
    {
        key = this.transform.parent.name + this.name;
    }
    private void Start()
    {
        LoadPointData();
        curPos = transform.position;
        SpawnByData();
    }

    private void SpawnByData()
    {
        if (pointData.nameGun == "null") return;

        this.Gun = GunManager.Instance.SpawnGunN(pointData.nameGun);
        this.Gun.SetParent(this.transform);
        this.Gun.localPosition = Vector3.zero;
        this.Gun.gameObject.SetActive(true);
        this.Gun.GetComponent<GunData>().setData(pointData.lvGun);
    }

    public void EndSelected()
    {
        if(PointSwap != null)
        {
            //this.GunSetParent(PointSwap);
            Transform thisGun = this.Gun;
            if(PointSwap.Gun != null)
            {
                if(this.GunData.Level == PointSwap.GunData.Level && this.GunData.id == PointSwap.GunData.id)
                {
                    //Debug.Log("merge");
                    //this.Gun = null;
                    ObjectPooler.EnqueueObject(this.Gun,GunManager.Instance.PoolParent, this.Gun.name);
                    ObjectPooler.EnqueueObject(PointSwap.Gun, GunManager.Instance.PoolParent, PointSwap.Gun.name);

                    int nextLevel = this.GunData.Level + 1;
                    this.Gun = null;

                    PointSwap.Gun = GunManager.Instance.SpawnGunN(PointSwap.Gun.name);
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
            PointSwap.SavePointData();
        }
        this.transform.position = curPos;
        SavePointData();

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

    public void SavePointData()
    {
        string  nameGun = this.Gun?.name ?? "null";
        int lvGun = this.GunData?.Level ?? 0;
        pointData = new PointData(nameGun, lvGun);
        string stringPointData = JsonUtility.ToJson(pointData).ToString();
        PlayerPrefs.SetString(this.transform.parent.name + this.name, stringPointData);
    }

    private void LoadPointData()
    {
        if (!PlayerPrefs.HasKey(key)) return;
        string stringJson = PlayerPrefs.GetString(key);
        JsonUtility.FromJsonOverwrite(stringJson, pointData);
    }
}

public class PointData
{
    public string nameGun;
    public int lvGun;

    public PointData()
    {
        this.nameGun = "null";
        this.lvGun = 0; 
    }

    public PointData(string nameGun, int lvGun)
    {
        this.nameGun = nameGun;
        this.lvGun = lvGun;
    }
}
