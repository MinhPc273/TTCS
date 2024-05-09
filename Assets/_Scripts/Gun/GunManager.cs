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
    [SerializeField] private List<PointSpawn> listPointSpawn = new();
    private int indexPoint;

    private void Awake()
    {
        Instance = this;
        this.LoadPointSpawn();
        indexPoint = 0;
        SetUpPool();
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


    public void SpawnGun()
    {
        indexPoint = IndexPoint();
        if(indexPoint == -1)
        {
            //Debug.Log("Full");
            return;
        }
        PointSpawn pointSpawn = listPointSpawn[indexPoint];
        //Transform gunSpawn =  Instantiate(GunPrefab, pointSpawn.transform.position, pointSpawn.transform.localRotation);

        Transform _gunSpawn = ListGunPrefab[(int)Random.Range(0, ListGunPrefab.Count)];

        Transform gunSpawn = ObjectPooler.DequeueObject<Transform>(_gunSpawn.name,_gunSpawn);
        gunSpawn.gameObject.SetActive(true);
        gunSpawn.SetParent(pointSpawn.transform);
        gunSpawn.localPosition = Vector3.zero;
        pointSpawn.Gun = gunSpawn;
        pointSpawn.SavePointData();
    }

    private int IndexPoint()
    {
        int index = 0;
        foreach(PointSpawn t in listPointSpawn)
        {
            if (t.Gun == null)
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    public Transform SpawnGunN(string key)
    {
        //Transform _gunSpawn = ObjectPooler.DequeueObject<Transform>("Gun");
        return ObjectPooler.DequeueObject<Transform>(key);
    }
}
