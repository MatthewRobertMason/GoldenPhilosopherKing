using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuoteContainer
{
    public string unityTitle;

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
