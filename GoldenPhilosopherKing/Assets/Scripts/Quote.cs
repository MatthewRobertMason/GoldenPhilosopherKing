using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class Quote
{
    [XmlElement("QuoteAuthor")]
    public string quoteAuthor;
    [XmlElement("Quote")]
    public string quote;
    [XmlElement("AudioName")]
    public string audioFilename;
    [XmlIgnore]
    public AudioClip quoteAudio;
    [Range(0, 2)]
    [XmlElement("DistortionLevel")]
    public int distortionLevel;

    public override string ToString()
    {
        return quoteAuthor + " - " + quote;
    }
}