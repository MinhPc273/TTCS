using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyDataBase;
    [SerializeField] EnemyAnimController enemyAnimController;

    public EnemySO EnemyDataBase => enemyDataBase;
    public EnemyAnimController EnemyAnimController => enemyAnimController;

}
