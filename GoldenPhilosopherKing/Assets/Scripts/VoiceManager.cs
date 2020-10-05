using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioManager audioManager;
    private Session session;

    [Range(0.0f, 1.0f)]
    public float volumeMultiplier = 1.0f;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    [Range(0.0f, 1.0f)]
    public float audioMultiplierWhenPlaying = 0.33f;

    public bool IsPlaying
    {
        get { return audioSource.isPlaying; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
        session = FindObjectOfType<Session>();


        if (audioSource == null)
        {
            Debug.LogError("VoiceManager: audioSource is null");
        }

        if (audioManager == null)
        {
            Debug.LogError("VoiceManager: audioManager is null");
        }

        if (session == null)
        {
            Debug.LogError("VoiceManager: audioManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.clip){
            audioSource.volume = volume * volumeMultiplier;

            if (audioSource.time == audioSource.clip.length)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

            if (audioSource.isPlaying)
            {
                audioManager.SetVolumeModifier(audioMultiplierWhenPlaying);
            }
            else
            {
                audioManager.ReSetVolumeModifier();
            }
        }
    }

    public void StartVoice()
    {
        audioSource.Play();
    }

    public void StopVoice()
    {
        audioSource.Pause();
    }

    public void SetVolume(float setVolume)
    {
        volume = setVolume;
    }

    public void SetVolumeModifier(float multiplier)
    {
        volumeMultiplier = multiplier;
    }

    public void PlayVoice(AudioClip voiceClip)
    {
        audioSource.clip = voiceClip;
        audioSource.Play();
    }
}
