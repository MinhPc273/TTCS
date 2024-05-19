using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] Enemy EnemyPrefab;

    [SerializeField] List<Enemy> ListBoss;

    [SerializeField] int AmountEnemyInWave;

    [SerializeField] float timeSawn;

    [SerializeField] Transform enemyParent;

    public Transform EnemyParent => enemyParent;

    private int index;

    private int Level;

    private int enemyExits;


    private void Awake()
    {
        Instance = this;

        ObjectPooler.SetupPool(EnemyPrefab, enemyParent, AmountEnemyInWave, EnemyPrefab.name);
    }

    public IEnumerator StartWave(float timeDelay)
    {
        index = 0;
        enemyExits = AmountEnemyInWave;
        Level = Prefs.Level;
        yield return new WaitForSeconds(timeDelay);
        if(Level % 10 == 0)
        {
            StartCoroutine(EnableBoss());
        }
        else
        {
            StartCoroutine(EnableEnemy());
        }
        yield return null;
    }

    IEnumerator EnableEnemy()
    {
        Enemy enemy = ObjectPooler.DequeueObject<Enemy>(EnemyPrefab.name, EnemyPrefab, enemyParent);
        enemy.EnemyDataBase.stats.getValueByLevel(Level);
        enemy.sortingLayer.sortingOrder = -index;
        yield return new WaitForSeconds(timeSawn);
        if(++index == AmountEnemyInWave)
        {
            yield break;
        }
        else
        {
            yield return StartCoroutine(EnableEnemy());
        }
    }

    IEnumerator EnableBoss()
    {
        Enemy WillBoss = getBoss();
        Enemy boss = ObjectPooler.DequeueObject<Enemy>(WillBoss.name, WillBoss, enemyParent);
        boss.EnemyDataBase.stats.getValueBossByLevel(Level);
        Debug.Log("Boss");
        yield break;
    }

    private Enemy getBoss()
    {
        foreach(Enemy boss in ListBoss)
        {
            if (!PlayerPrefs.HasKey("called" + boss.name))
            {
                PlayerPrefs.SetInt("called" + boss.name, 1);
                return boss;
            }
        }
        return ListBoss[Random.Range(0, ListBoss.Count)];
    }

    public void KillEnemy()
    {
        enemyExits--;
        if(enemyExits == 0)
        {
            GameManager.Instance.Win();
        }
    }

    public void KillBoss()
    {
        GameManager.Instance.Win();
    }
}

