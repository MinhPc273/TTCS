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
        if(turretType == TurretAI.TurretType.Single)
        {
            _move.Run_Slow();
        }

        CurHP -= ATk;
        HpEnemyUI hpEnemyUI = ObjectPooler.DequeueObject(HpEnemyUIPrefab.name, HpEnemyUIPrefab);
        hpEnemyUI.transform.position = HpEnemyTransform.position;
        hpEnemyUI.setValue(ATk, turretType);
        hpEnemyUI.gameObject.SetActive(true);
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
        _enemyAnimController.PlayAnim(StateAnim.Die);
        isDead = true;
        StartCoroutine(Destoy());
    }

    IEnumerator Destoy()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        CoinReward coinReward = ObjectPooler.DequeueObject(GUIManager.Instance.CoinRewardPrefab.name, GUIManager.Instance.CoinRewardPrefab, GUIManager.Instance.CoinRewardParent);
        coinReward.setData(this.transform, CurrentEnemyStats.Coin); 
        coinReward.gameObject.SetActive(true);
        ObjectPooler.EnqueueObject(this.GetComponent<Enemy>(),WaveManager.Instance.EnemyParent, this.name);
        yield break;
    }

    public void Disable()
    {
        ObjectPooler.EnqueueObject(this.GetComponent<Enemy>(), WaveManager.Instance.EnemyParent, this.name);
    }
}
