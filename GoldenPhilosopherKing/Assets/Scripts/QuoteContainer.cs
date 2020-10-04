using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;

[System.Serializable]
public class QuoteContainer
{
    [XmlElement("UnityTitle")]
    public string unityTitle;

    [XmlElement("Quotes")]
    public Quote[] quotes;

    public Quote GetQuote(int distortionLevel)
    {
        foreach (Quote q in quotes)
        {
            if (q.distortionLevel == distortionLevel)
            {
                return q;
            }
        }

        return null;
    }
}
