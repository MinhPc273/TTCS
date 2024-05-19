using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GunManager Instance;

    [SerializeField] private List<Transform> ListGunPrefab;
    [SerializeField] private Transform PointSpawnParent;
    public Transform PoolParent;
    public Transform EffLevelUp;
    [SerializeField] private List<PointSpawn> listPointSpawn = new();

    [SerializeField] private List<PosGunUnlock> listPosGunUnlock = new();
    private int indexPoint;

    private int gunIndex;

    Transform gunSpawn;
    private void Awake()
    {
        Instance = this;
        this.LoadPointSpawn();
        indexPoint = 0;
        SetUpPool();
        gunIndex = 0;

    }

    private void Start()
    {
        SpawnGunUI();
    }

    private void LoadPointSpawn()
    {
        foreach (Transform t in PointSpawnParent)
        {
            listPointSpawn.Add(t.GetComponent<PointSpawn>());
        }
    }

    void SetUpPool()
    {
        for(int i = 0; i < ListGunPrefab.Count; i++)
        {
            ObjectPooler.SetupPool(ListGunPrefab[i],PoolParent, 20, ListGunPrefab[i].name);
        }
    }


    public void SpawnGunUI()
    {
        if(Prefs.Level < 3)
        {
            gunIndex = 1;
        }
        else if(Prefs.Level < 5)
        {
            gunIndex = 2;
        }
        else
        {
            gunIndex = 3;
        }
        Transform _gunSpawn = ListGunPrefab[(int)Random.Range(0, gunIndex)];
        gunSpawn = ObjectPooler.DequeueObject<Transform>(_gunSpawn.name, _gunSpawn);
        gunSpawn.gameObject.SetActive(false);
        GUIManager.Instance.setGunUI(gunSpawn.GetComponent<Gun>().GunDataBase.Icon);
    }

    public void SpawnGun()
    {
        indexPoint = IndexPoint();
        if (indexPoint == -1)
        {
            //Debug.Log("Full");
            return;
        }
        PointSpawn pointSpawn = listPointSpawn[indexPoint];
        gunSpawn.gameObject.SetActive(true);
        gunSpawn.SetParent(pointSpawn.transform);
        gunSpawn.localPosition = Vector3.zero;
        gunSpawn.GetComponent<GunData>().turretAI.EndSlected(pointSpawn.CanFire);
        pointSpawn.Gun = gunSpawn;
        pointSpawn.SavePointData();
        SpawnGunUI();
    }

    private int IndexPoint()
    {
        int index = 0;
        foreach(PointSpawn t in listPointSpawn)
        {
            if (t.Gun == null && t.IsBuy)
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    public void checkUnlock()
    {
        PointSpawn pointSpawn = listPosGunUnlock.Find(x => x.levelUnlock == Prefs.Level)?.PosGun;
        if(pointSpawn != null)
        {
            pointSpawn.Unlock();
        }
    }
}

[System.Serializable]
public class PosGunUnlock
{
    public int levelUnlock;
    public PointSpawn PosGun;
}
