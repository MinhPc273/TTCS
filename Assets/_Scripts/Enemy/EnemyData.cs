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

    }

    private void OnEnable()
    {
        CurrentEnemyStats = _enemyData.stats.getValueByLevel(Prefs.Level);
        CurHP = MaxHP = CurrentEnemyStats.HP;
        isDead = false;
        Slider.value = CurHP / MaxHP;
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
        if(_enemyData.isBoss)
        {
            WaveManager.Instance.KillBoss();
        }
        else
        {
            WaveManager.Instance.KillEnemy();
        }
        _enemyAnimController.PlayAnim(State.Die);
        isDead = true;
        StartCoroutine(Destoy());
    }

    IEnumerator Destoy()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ObjectPooler.EnqueueObject(this.GetComponent<Enemy>(), this.name);
        Debug.Log(this.name);
        yield break;
    }
}
