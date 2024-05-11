using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : EnemyAlbility
{
    public BaseEnemyStats CurrentEnemyStats;

    [Header("Heath Bar")]
    [SerializeField] Slider Slider;
    [SerializeField] HpEnemyUI HpEnemyUIPrefab;
    [SerializeField] Transform HpEnemyTransform;
    private float MaxHP;
    private float CurHP;

    private bool isDead;

    public bool IsDead => isDead;

    protected override void Initialization()
    {
        base.Initialization();
        CurrentEnemyStats = _enemyData.stats.getValueByLevel(1);
        CurHP = MaxHP = CurrentEnemyStats.HP;
        isDead = false;
    }

    public void GetDamage(float ATk, TurretAI.TurretType turretType)
    {
        CurHP -= ATk;
        HpEnemyUI hpEnemyUI = ObjectPooler.DequeueObject(HpEnemyUIPrefab.name, HpEnemyUIPrefab);
        //HpEnemyUI hpEnemyUI = Instantiate(HpEnemyUIPrefab);
        hpEnemyUI.transform.position = HpEnemyTransform.position;
        hpEnemyUI.setValue(ATk, turretType);
        if(CurHP <= 0)
        {
            CurHP = 0;
            EnemyDead();
        }
        Slider.value = CurHP/MaxHP;
    }

    private void EnemyDead()
    {
        if (isDead) return;
        _enemyAnimController.PlayAnim(State.Die);
        isDead = true;
        //animation Dead
    }
}
