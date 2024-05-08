using UnityEngine;

public static class Prefs
{
    public static int Coin
    {
        set => PlayerPrefs.SetInt(PrefsConst.COIN, value);
        get => PlayerPrefs.GetInt(PrefsConst.COIN, 0);
    }
}
