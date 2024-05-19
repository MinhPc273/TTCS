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
    public float Coin;

    public BaseEnemyStats(float hP, float speed, float coin)
    {
        HP = hP;
        Speed = speed;
        Coin = coin;
    }

    public BaseEnemyStats getValueByLevel(int Level)
    {
        float hp = this.HP * Mathf.Pow(1.2f, Level -1);
        return new BaseEnemyStats(hp, this.Speed, this.Coin);
    }

    public BaseEnemyStats getValueBossByLevel(int Level)
    {
        float hp = this.HP * Mathf.Pow(1.8f, Level - 1);
        return new BaseEnemyStats(hp, this.Speed, this.Coin);
    }
}
