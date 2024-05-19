using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : GunAlbility
{
    public int id;
    public int Level;
    public BaseStatsGun CurentStatsGun;

    public TurretAI turretAI;

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
        id = _gunDataBase._id;
        CurentStatsGun = _gunDataBase._baseStatsGuns.curentStatGun(l);
        _gunUI.setTxtLevel(l);

        turretAI.attackDamage = CurentStatsGun._ATK;
        turretAI.attackDist = CurentStatsGun._rangeATK;
        turretAI.shootCoolDown = CurentStatsGun._speedATK;
    }
}
