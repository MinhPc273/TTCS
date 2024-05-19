using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyDataBase;
    [SerializeField] AnimController enemyAnimController;
    public SortingGroup sortingLayer;

    public EnemySO EnemyDataBase => enemyDataBase;
    public AnimController EnemyAnimController => enemyAnimController;

}
