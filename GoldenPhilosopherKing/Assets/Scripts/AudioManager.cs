using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] music;

    public AudioClip startAudio;

    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

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

    public void SetVolume()
    {

    }
}
