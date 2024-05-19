using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;

    [SerializeField] GunManager gunManager;

    [SerializeField] WaveManager waveManager;

    [SerializeField] GUIManager gui;

    private int indexLose;

    private void Awake()
    {
        Instance = this;
        indexLose = 0;
    }

    public void SpawnGun()
    {
        gunManager.SpawnGun();
    }
    
    public void NewWave(float timeDelay)
    {
        StartCoroutine(waveManager.StartWave(timeDelay));
    }

    public void Win()
    {
        Prefs.Level++;
        indexLose = 0;
        gunManager.checkUnlock();
        gui.NextLevelUI();
    }

    public void Lose()
    {
        indexLose++;
        if(Prefs.Level % 10 != 1 && indexLose >= 2)
        {
            Prefs.Level--;
            indexLose = 0;
            gui.PrevLevelUI();
        }
    }
}
