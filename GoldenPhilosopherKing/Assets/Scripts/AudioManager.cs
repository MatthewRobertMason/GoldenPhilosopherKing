using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private int currentTrack = 0;
    public AudioClip[] music;

    public AudioClip startAudio;

    [Range(0.0f, 30.0f)]
    public float fadeTime = 10.0f;

    [Range(0.0f, 60.0f)]
    public float audioTrackCutOff = 5.0f;

    private float initialVolumnMultiplier = 1.0f;
    [Range(0.0f, 1.0f)]
    public float volumeMultiplier = 1.0f;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    public bool enablePitchWalk = true;
    public int levelDelayUntilPitchWalk = 5;
    [Range(0.0f, 1)]
    public float pitchVariance = 0.0f;
    public float pitchVariancePerLevel = 0.01f;
    public float maxPitchVariance = 0.2f;
    [Range(0.0f, 2.0f)]
    public float pitchWalk = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        initialVolumnMultiplier = volumeMultiplier;

        if (startAudio != null)
        {
            audioSource.clip = startAudio;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("You forgot the starting music hoser");
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volume * volumeMultiplier;

        if (audioSource.time == audioSource.clip.length)
        {
            // Next song
            audioSource.clip = music[((++currentTrack) % music.Length)];
            audioSource.Play();
        }

        if (fadeTime > 0)
        { 
            if (audioSource.time > audioSource.clip.length - fadeTime)
            {
                audioSource.volume = volume * volumeMultiplier * Mathf.Lerp(1.0f, 0.0f, (audioSource.time - (audioSource.clip.length - fadeTime)) / fadeTime);
            }

            if (audioSource.time < fadeTime)
            {
                audioSource.volume = volume * volumeMultiplier * Mathf.Lerp(0.0f, 1.0f, (audioSource.time / fadeTime));
            }
        }

        if (enablePitchWalk)
        {
            if (pitchVariance > maxPitchVariance)
            {
                pitchVariance = maxPitchVariance;
            }

            if (pitchVariance > 0)
            {
                float walk = 0.0f; 

                walk = Mathf.Sin(Time.fixedTime/10.0f) * (Mathf.Sin((3 * Time.fixedTime)/10.0f)) * Mathf.Sin((5 / 3 * Time.fixedTime)/10.0f);
                walk = walk * pitchVariance;
                
                pitchWalk = Mathf.Clamp(1.0f + walk, 1.0f - pitchVariance, 1.0f + pitchVariance);
                audioSource.pitch = pitchWalk;
            }
            else
            {
                audioSource.pitch = 1.0f;
            }
        }
        else
        {
            audioSource.pitch = 1.0f;
        }
    }

    public void StartMusic()
    {
        if (audioSource.time > 0.0f)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Pause();
    }
    
    public void Pause()
    {
        audioSource.Pause();
    }

    public void SetVolume(float setVolume)
    {
        volume = setVolume;
    }

    public void SetVolumeModifier (float multiplier)
    {
        volumeMultiplier = multiplier;
    }

    public void ReSetVolumeModifier()
    {
        volumeMultiplier = initialVolumnMultiplier;
    }
}
