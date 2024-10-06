using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioMixer audioMixer;
    public AudioSource soundFXObject;
    
    void Awake()
    {
        if(instance == null)
        {   
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayAudioClip(AudioClip audioClip, AudioMixerGroup output, Transform spawnTransform,float volume = 1f,float customLength = 0f, float pitch = 1f)
    {
        if(audioClip == null) return;
        AudioSource audioSource = Instantiate(soundFXObject,spawnTransform.position,Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.pitch = pitch;

        audioSource.outputAudioMixerGroup = output;

        audioSource.Play();

        float audioLength = audioClip.length;
        if(customLength != 0) audioLength = customLength;

        Destroy(audioSource.gameObject,audioLength);
    }

    public void PlayRandomAudioClip(AudioClip[] audioClip, AudioMixerGroup output, Transform spawnTransform, float volume = 1f, float pitch = 1f)
    {

        AudioSource audioSource = Instantiate(soundFXObject,spawnTransform.position,Quaternion.identity);

        int rand = Random.Range(0,audioClip.Length);
        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = output;

        audioSource.Play();

        float audioLength = audioClip[rand].length;

        Destroy(audioSource.gameObject,audioLength);
    }
}
