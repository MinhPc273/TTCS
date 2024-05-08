using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObject/GunData")]
public class GunSO : ScriptableObject
{
    public int _id;
    public int _level;
    public BaseStatsGun _baseStatsGuns;
}

[Serializable]
public class BaseStatsGun
{
    public float _ATK;
    public float _rangeATK;
    public float _speedATK;

    public BaseStatsGun(float ATK, float rangeATK, float speedATK)
    {
        this._ATK = ATK;
        this._rangeATK = rangeATK;
        this._speedATK = speedATK;
    }

    public BaseStatsGun curentStatGun(int level)
    {
        float ATK = _ATK * (1 + 0.2f * (level - 1));
        return new BaseStatsGun(ATK, _rangeATK, _speedATK);   
    }
}
