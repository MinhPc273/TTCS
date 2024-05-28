using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;

    private void OnEnable()
    {
        _sliderMusic.value = Prefs.VolumeMusic;
        _sliderSFX.value = Prefs.SFXMusic;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
