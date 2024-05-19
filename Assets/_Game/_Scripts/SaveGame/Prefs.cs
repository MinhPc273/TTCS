using System;
using UnityEngine;

public static class Prefs
{
    public static int Coin
    {
        set => PlayerPrefs.SetInt(PrefsConst.COIN, value);
        get => PlayerPrefs.GetInt(PrefsConst.COIN, 100);
    }

    public static int CoinRequired
    {
        set => PlayerPrefs.SetInt(PrefsConst.COIN_REQUIRED, value);
        get => PlayerPrefs.GetInt(PrefsConst.COIN_REQUIRED, 50 * (int)Mathf.Pow(2, (Level - 1)/10));
    }

    public static int Level
    {
        set => PlayerPrefs.SetInt(PrefsConst.LEVEL, value);
        get => PlayerPrefs.GetInt(PrefsConst.LEVEL, 1);
    }
}
