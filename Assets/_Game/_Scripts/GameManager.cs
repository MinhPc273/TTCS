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
    private bool isLose;

    private void Awake()
    {
        Application.targetFrameRate = 144;
        Instance = this;
        indexLose = 0;
        isLose = false;
    }

    public void SpawnGun()
    {
        gunManager.SpawnGun();
    }
    
    public void NewWave(float timeDelay)
    {
        StartCoroutine(waveManager.StartWave(timeDelay));
        isLose = false;
    }

    public void Win()
    {
        Prefs.Level++;
        indexLose = 0;
        gunManager.checkUnlock();
        gui.LoadLevelUI();
        //gui.Win();
    }

    public void Lose()
    {
        if (isLose) return;
        isLose = true;
        indexLose++;
        if (Prefs.Level % 10 != 1 && indexLose >= 2)
        {
            Prefs.Level--;
            indexLose = 0; 
        }
        gui.LoadLevelUI();
    }
}
