using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : GunAlbility
{
    public int id;
    public int Level;
    public BaseStatsGun CurentStatsGun;

    //private GunSO gunData;

    //private Gun _gun;

    protected override void Initialization()
    {
        base.Initialization();
    }

    private void OnEnable()
    {
        this.setData(1);
    }
    public void setData(int l) 
    {
        Level = l;
        id = _gunData._id;
        CurentStatsGun = _gunData._baseStatsGuns.curentStatGun(l);
        _gunUI.setTxtLevel(l);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
