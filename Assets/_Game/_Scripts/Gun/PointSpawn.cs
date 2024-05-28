using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    private enum Type
    {
        PointSpawn,
        PointPos
    }
     
    [SerializeField] private Type type;

    public Transform Gun;
    public bool canShoot;

    public Vector3 curPos;
    [SerializeField] private PointSpawn PointSwap;
    [SerializeField] private bool canFire;

    [Header("UI")]
    [SerializeField] private GameObject Lock;
    [SerializeField] private GameObject Add;
    [SerializeField] private GameObject ArrowDown;

    [SerializeField] private bool isUnlock;
    [SerializeField] private bool isBuy;
    public bool IsUnlock => isUnlock;
    public bool IsBuy => isBuy;

    public bool CanFire => canFire;
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
        if (!isUnlock)
        {
            if(Lock != null)
            {
                Lock.SetActive(true);
            }
            return;
        }
        if (pointData.nameGun == "null")
        {
            //ArrowDown?.SetActive(true);
            if(type == Type.PointPos)
            {
                if (ArrowDown != null)
                {
                    ArrowDown.SetActive(true);
                }
            }
            else if(type == Type.PointSpawn && !isBuy)
            {
                if (Add != null)
                {
                    Add.SetActive(true);
                }
            }
            return;
        }

        this.Gun = ObjectPooler.DequeueObject<Transform>(pointData.nameGun); ;
        this.Gun.SetParent(this.transform);
        this.Gun.localPosition = Vector3.zero;
        this.Gun.gameObject.SetActive(true);
        this.Gun.GetComponent<GunData>().setData(pointData.lvGun);
        this.Gun.GetComponent<GunData>().turretAI.EndSlected(this.canFire);
    }

    public void StartSelected()
    {
        TurretAI turretGun =  this.Gun.GetComponent<GunData>().turretAI;
        turretGun.Selected();
    }

    public void EndSelected()
    {
        if(PointSwap != null && PointSwap.IsUnlock && PointSwap.IsBuy)
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

                    PointSwap.Gun = ObjectPooler.DequeueObject<Transform>(PointSwap.Gun.name); ;
                    PointSwap.Gun.SetParent(PointSwap.transform);
                    PointSwap.Gun.localPosition = Vector3.zero;
                    PointSwap.Gun.gameObject.SetActive(true);
                    PointSwap.Gun.GetComponent<GunData>().setData(nextLevel);
                    PointSwap.Gun.GetComponent<GunData>().turretAI.EndSlected(PointSwap.CanFire);
                    //Instantiate(GunManager.Instance.EffLevelUp, PointSwap.transform.position, GunManager.Instance.EffLevelUp.rotation);
                    VFXPool vfx = ObjectPooler.DequeueObject(GunManager.Instance.EffLevelUp.name, GunManager.Instance.EffLevelUp);
                    vfx.transform.SetPositionAndRotation(PointSwap.transform.position, GunManager.Instance.EffLevelUp.transform.rotation);
                    vfx.gameObject.SetActive(true);

                    if(TutorialManager.Instance.gameObject.activeInHierarchy)
                    {
                        TutorialManager.Instance.Step3(PointSwap.Gun);
                    }

                }
                else
                {
                    this.Gun = PointSwap.Gun;
                    this.Gun.SetParent(this.transform);
                    this.Gun.localPosition = Vector3.zero;
                    this.Gun.GetComponent<GunData>().turretAI.EndSlected(canFire);

                    PointSwap.Gun = thisGun;
                    PointSwap.Gun.SetParent(PointSwap.transform);
                    PointSwap.Gun.localPosition = Vector3.zero;
                    PointSwap.Gun.GetComponent<GunData>().turretAI.EndSlected(PointSwap.CanFire);
                }
            }
            else 
            {
                this.Gun = null;

                PointSwap.Gun = thisGun;
                PointSwap.Gun.SetParent(PointSwap.transform);
                PointSwap.Gun.localPosition = Vector3.zero;
                PointSwap.Gun.GetComponent<GunData>().turretAI.EndSlected(PointSwap.CanFire);
            }
            PointSwap.SavePointData();
        }
        this.transform.position = curPos;
        this.Gun?.GetComponent<GunData>().turretAI.EndSlected(this.canFire);
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
        string  nameGun;
        int lvGun = 0;
        if(this.Gun != null)
        {
            nameGun = this.Gun.name;
            if(ArrowDown != null)
            {
                ArrowDown.SetActive(false);
            }
        }
        else
        {
            nameGun = "null";
            if (ArrowDown != null)
            {
                ArrowDown.SetActive(true);
            }
        }
        if(this.GunData != null)
        {
            lvGun = this.GunData.Level;
        }
        pointData = new PointData(nameGun, lvGun, isUnlock, isBuy);
        string stringPointData = JsonUtility.ToJson(pointData).ToString();
        Debug.Log(stringPointData); 
        PlayerPrefs.SetString(this.transform.parent.name + this.name, stringPointData);
    }

    private void LoadPointData()
    {
        if (!PlayerPrefs.HasKey(key)) return;
        string stringJson = PlayerPrefs.GetString(key);
        JsonUtility.FromJsonOverwrite(stringJson, pointData);
        isUnlock = pointData.isUnlock;
        isBuy = pointData.isBuy;
    }

    public void Buy()
    {
        isBuy = true;
        SavePointData();
    }

    public void Unlock()
    {
        if (!isUnlock)
        {
            isUnlock = true;
            Lock.SetActive(false);
            if(type == Type.PointPos)
            {
                ArrowDown.SetActive(true);
            }
            else
            {
                Add.SetActive(true);
            }
            SavePointData() ;
        }
    }
}

public class PointData
{
    public bool isUnlock;
    public bool isBuy;
    public string nameGun;
    public int lvGun;

    public PointData()
    {
        isUnlock = false;
        isBuy = false;
        this.nameGun = "null";
        this.lvGun = 0; 
    }

    public PointData(string nameGun, int lvGun, bool isUnlock, bool isBuy)
    {
        this.nameGun = nameGun;
        this.lvGun = lvGun;
        this.isUnlock = isUnlock;
        this.isBuy = isBuy;
    }
}
