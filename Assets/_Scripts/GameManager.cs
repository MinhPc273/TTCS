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

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        NewWave();
    }

    public void SpawnGun()
    {
        gunManager.SpawnGun();
    }
    
    public void NewWave()
    {
        waveManager.StartWave();
    }

    public void Win()
    {
        Prefs.Level++;
        this.NewWave();
        gui.NextLevelUI();
    }

    public void Lose()
    {
        if(Prefs.Level % 10 != 1)
        {
            Prefs.Level--;
        }
        this.NewWave();
        gui.PrevLevelUI();
    }
}
