using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    void OnEnable()
    {
        float musicVolume;
        if (audioMixer.GetFloat("music", out musicVolume))
        {
            // Debug.Log("Current Music Volume: " + musicVolume);
        }
        else
        {
            Debug.LogError("Failed to get the volume parameter.");
        }
        musicSlider.value = Mathf.Pow(10, musicVolume / 20);

        float sfxVolume;
        if (audioMixer.GetFloat("sfx", out sfxVolume))
        {
            // Debug.Log("Current SFX Volume: " + sfxVolume);
        }
        else
        {
            Debug.LogError("Failed to get the volume parameter.");
        }
        sfxSlider.value = Mathf.Pow(10, sfxVolume / 20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music",Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx",Mathf.Log10(volume)*20);
    }
}
