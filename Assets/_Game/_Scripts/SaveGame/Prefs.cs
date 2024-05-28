using System;
using UnityEngine;
using static Animancer.Validate;

public static class Prefs
{
    public static int Coin
    {
        //set => PlayerPrefs.SetInt(PrefsConst.COIN, value);
        set
        {
            PlayerPrefs.SetInt(PrefsConst.COIN, value);
            GUIManager.Instance.SetUI();
        }
        get => PlayerPrefs.GetInt(PrefsConst.COIN, 100);
    }

    public static int CoinRequired
    {
        //set => PlayerPrefs.SetInt(PrefsConst.COIN_REQUIRED, value);
        set
        {
            PlayerPrefs.SetInt(PrefsConst.COIN_REQUIRED, value);
            GUIManager.Instance.SetUI();
        }
        get => PlayerPrefs.GetInt(PrefsConst.COIN_REQUIRED, 50);
    }

    public static int LvGunSpawn
    {
        set
        {
            PlayerPrefs.SetInt(PrefsConst.LV_GUN_SPAWN, value);
            GUIManager.Instance.TxtLvGun.text = "Lv." + value;
        }
        get => PlayerPrefs.GetInt(PrefsConst.LV_GUN_SPAWN, 1);
    }

    public static int Level
    {
        set => PlayerPrefs.SetInt(PrefsConst.LEVEL, value);
        get => PlayerPrefs.GetInt(PrefsConst.LEVEL, 1);
    }

    public static int IsTutorials
    {
        set => PlayerPrefs.SetInt(PrefsConst.TUTORIALS, value);
        get => PlayerPrefs.GetInt(PrefsConst.TUTORIALS, 1);
    }

    public static string FormatIntValue(int value)
    {
        if (value < 1000)
        {
            return value.ToString();
        }
        if (value < 1000000)
        {
            int first = (int)(value / 1000);
            int second = (int)(value % 1000);
            return first + "K" + second;
        }
        if(value < 1000000000)
        {
            int first = (int)(value / 1000000);
            int second = (int)(value % 1000000);
            return first + "M" + second;
        }
        return value.ToString();
    }

    public static float VolumeMusic
    {
        set => PlayerPrefs.SetFloat(PrefsConst.MUSIC_VOLUME, value);
        get => PlayerPrefs.GetFloat(PrefsConst.MUSIC_VOLUME, 0.5f);
    }

    public static float SFXMusic
    {
        set => PlayerPrefs.SetFloat(PrefsConst.SFX_VOLUME, value);
        get => PlayerPrefs.GetFloat(PrefsConst.SFX_VOLUME, 0.5f);
    }
}
