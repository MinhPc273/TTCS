using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlbility : MonoBehaviour
{
    protected Enemy _enemy;
    protected EnemySO _enemyData;
    protected EnemyAnimController _enemyAnimController;
    void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _enemy = GetComponent<Enemy>();
        if (_enemy != null)
        {
            _enemyData = _enemy.EnemyDataBase;
            _enemyAnimController = _enemy.EnemyAnimController;
        }
    }


}
