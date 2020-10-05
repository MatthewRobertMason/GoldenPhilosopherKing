using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public bool audioMute = false;
    public float audioVolume = 0.25f;
    public float audioReductionWhenVoicePlaying = 0.25f;

    public bool voiceMute = false;
    public float voiceVolume = 0.5f;

    public GameObject options;
    public GameObject secretOptions;
    public GameObject superSecretoptions;

    #region Secret Options

    public bool blur = true;
    public bool pitchWarp = true;

    #region Super Secret Options

    public bool seeActualQuotes = false;

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
