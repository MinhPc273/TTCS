using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : GunAlbility
{
    public int Level;
    public BaseStatsGun CurentStatsGun;

    private GunSO gunData;

    //private Gun _gun;

    protected override void Initialization()
    {
        base.Initialization();
        if(_gunData != null)
        {
            Level = _gunData._level;
            CurentStatsGun = _gunData._baseStatsGuns.curentStatGun(Level);

            //UI
            _gunUI.setTxtLevel(Level);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
