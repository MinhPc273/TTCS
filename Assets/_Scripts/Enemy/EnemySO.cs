using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData")]
public class EnemySO : ScriptableObject
{
    public int id;
    public BaseEnemyStats stats;
}

[Serializable]
public class BaseEnemyStats
{
    public float HP;
    public float Speed;
    public float Coin;
    public float CoinDel;

    public BaseEnemyStats(float hP, float speed, float coin, float coinDel)
    {
        HP = hP;
        Speed = speed;
        Coin = coin;
        CoinDel = coinDel;
    }

    public BaseEnemyStats getValueByLevel(int Level)
    {
        float hp = this.HP * (1 + 0.25f * (Level - 1));
        return new BaseEnemyStats(hp, this.Speed, this.Coin, this.CoinDel);
    }
}
