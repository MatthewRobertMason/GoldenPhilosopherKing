using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quote
{
    public string quoteAuthor;
    public string quote;
    public AudioClip quoteAudio;

    public override string ToString()
    {
        return quoteAuthor + " - " + quote;
    }
}
