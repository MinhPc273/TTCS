using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GunManager Instance;

    [SerializeField] private Transform GunPrefab;
    [SerializeField] private Transform PointSpawnParent;
    public Transform PoolParent;
    [SerializeField] private List<PointSpawn> listPointSpawn = new();
    private int indexPoint;

    private void Awake()
    {
        Instance = this;
        this.LoadPointSpawn();
        indexPoint = 0;
    }

    private void LoadPointSpawn()
    {
        foreach (Transform t in PointSpawnParent)
        {
            listPointSpawn.Add(t.GetComponent<PointSpawn>());
        }
    }

    void Start()
    {
        SetUpPool();
    }

    void SetUpPool()
    {
        ObjectPooler.SetupPool(GunPrefab,PoolParent, 20, "Gun");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Transform gunSpawn = ObjectPooler.DequeueObject<Transform>("Gun");
        gunSpawn.gameObject.SetActive(true);
        gunSpawn.SetParent(pointSpawn.transform);
        gunSpawn.localPosition = Vector3.zero;
        pointSpawn.Gun = gunSpawn;
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

    public Transform SpawnGunN()
    {
        //Transform _gunSpawn = ObjectPooler.DequeueObject<Transform>("Gun");
        return ObjectPooler.DequeueObject<Transform>("Gun");
    }
}
