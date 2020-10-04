using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quote
{
    public string quoteAuthor;
    public string quote;
    public AudioClip quoteAudio;
    [Range(0, 2)]
    public int distortionLevel;

    public override string ToString()
    {
        return quoteAuthor + " - " + quote;
    }
}