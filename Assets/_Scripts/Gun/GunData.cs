using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    public int Level;
    public BaseStatsGun CurentStatsGun;

    private Gun _gun;

    void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        _gun = GetComponent<Gun>();
        if( _gun != null )
        {
            Level = _gun.GunData._level;
            CurentStatsGun = _gun.GunData._baseStatsGuns.curentStatGun(Level);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
