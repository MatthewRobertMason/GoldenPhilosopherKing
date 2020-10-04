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

    #region Secret Options

    public bool blur = true;
    public bool pitchWarp = true;

    #region Super Secret Options

    public bool seeActualQuotes = false;

    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
