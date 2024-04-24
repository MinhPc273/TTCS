using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform GunPrefab;
    [SerializeField] private Transform PointSpawnParent;
    [SerializeField] private List<PointSpawn> listPointSpawn = new();
    private int indexPoint;

    private void Awake()
    {
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
            Debug.Log("Full");
            return;
        }
        PointSpawn pointSpawn = listPointSpawn[indexPoint];
        Transform gunSpawn =  Instantiate(GunPrefab, pointSpawn.transform.position, pointSpawn.transform.localRotation);
        gunSpawn.SetParent(pointSpawn.transform);
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
}
