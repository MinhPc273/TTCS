using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyDataBase;
    [SerializeField] EnemyAnimController enemyAnimController;
    public SortingGroup sortingLayer;

    public EnemySO EnemyDataBase => enemyDataBase;
    public EnemyAnimController EnemyAnimController => enemyAnimController;

}
