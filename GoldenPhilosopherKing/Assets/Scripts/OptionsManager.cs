using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public GameObject options;
    public GameObject secretOptions;
    public GameObject superSecretoptions;

    public bool audioMute = false;
    public bool AudioMute
    {
        get { return audioMute; }
        set { audioMute = value; }
    }

    public float audioVolume = 0.25f;
    public float AudioVolume
    {
        get { return audioVolume; }
        set { audioVolume = value; }
    }
    public float audioReductionWhenVoicePlaying = 0.25f;
    public float AudioReductionWhenVoicePlaying
    {
        get { return audioReductionWhenVoicePlaying; }
        set { audioReductionWhenVoicePlaying = value; }
    }

    public bool voiceMute = false;
    public bool VoiceMute
    {
        get { return voiceMute; }
        set { voiceMute = value; }
    }
    public float voiceVolume = 0.5f;
    public float VoiceVolume
    {
        get { return voiceVolume; }
        set { voiceVolume = value; }
    }

    #region Secret Options

    public bool blur = true;
    public bool Blur
    {
        get { return blur; }
        set { blur = value; }
    }
    public bool pitchWarp = true;
    public bool PitchWarp
    {
        get { return pitchWarp; }
        set { pitchWarp = value; }
    }

    #region Super Secret Options

    public bool seeActualQuotes = false;
    public bool SeeActualQuotes
    {
        get { return seeActualQuotes; }
        set { seeActualQuotes = value; }
    }

    #endregion
    #endregion

    public void ToggleOptions()
    {
        options.gameObject.SetActive(!options.activeSelf);

        if (!options.activeSelf)
        {
            secretOptions.SetActive(false);
            superSecretoptions.SetActive(false);
        }
    }

    public void ToggleSecretOptions()
    {
        secretOptions.SetActive(!secretOptions.activeSelf);

        if (!secretOptions.activeSelf)
        {
            superSecretoptions.SetActive(false);
        }
    }

    public void ToggleSuperSecretOptions()
    {
        superSecretoptions.SetActive(!superSecretoptions.activeSelf);
    }



    
}
