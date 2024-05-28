using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        SetMusicVolume(Prefs.VolumeMusic);
        SetSFXVolume(Prefs.SFXMusic);
    }

    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("music", Mathf.Log10(level) * 20f);
        Prefs.VolumeMusic = level;
    }

    public void SetSFXVolume(float level)
    {
        _audioMixer.SetFloat("sfx", Mathf.Log10(level) * 20f);
        Prefs.SFXMusic = level;
    }
}
