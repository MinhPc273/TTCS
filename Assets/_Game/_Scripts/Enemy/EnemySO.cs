using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData")]
public class EnemySO : ScriptableObject
{
    public int id;
    public BaseEnemyStats stats;
    public bool isBoss;
}

[Serializable]
public class BaseEnemyStats
{
    public float HP;
    public float Speed;
    public int Coin;

    public BaseEnemyStats(float hP, float speed, int coin)
    {
        HP = hP;
        Speed = speed;
        Coin = coin;
    }

    public BaseEnemyStats getValueByLevel(int Level)
    {
        float hp = this.HP * Mathf.Pow(1.4f, Level -1);
        int coin = (int)(this.Coin * Mathf.Pow(1.2f, Level - 1));
        return new BaseEnemyStats(hp, this.Speed, coin);
    }

    public BaseEnemyStats getValueBossByLevel(int Level)
    {
        float hp = this.HP * Mathf.Pow(4f, Level - 1);
        int coin = (int)(this.Coin * Mathf.Pow(3f, Level - 1));
        return new BaseEnemyStats(hp, this.Speed, coin);
    }
}
