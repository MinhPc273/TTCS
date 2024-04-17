using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector3 currentPos;
    [SerializeField] private Vector3 newPos;

    [SerializeField] private PointSpawn PointSwap;
    [SerializeField] private Transform GunSwap;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSelected()
    {
        currentPos = transform.position;
    }

    public void EndSelected()
    {
        if(newPos != Vector3.one)
        {
            if(GunSwap != null)
            {
                GunSwap.transform.position = currentPos;
            }
            currentPos = newPos;
        }
        this.transform.position = currentPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PosGun")
        {
            newPos = other.transform.position;
            PointSwap = other.GetComponent<PointSpawn>();
            GunSwap = PointSwap.Gun;
            //GunSwap = other.GetComponent<PointSpawn>().Gun;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PosGun")
        {
            newPos = Vector3.one;
            GunSwap = null;
        }
    }

    private void Swap(PointSpawn p1, PointSpawn p2) 
    {

    }
}
