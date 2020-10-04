using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("RootQuotes")]
public class LoadQuotes
{
    [XmlElement("QuoteContainer")]
    public QuoteContainer[] quotes;

    [XmlIgnore]
    public bool saveDataToFile = false;

    public static void Serialize(LoadQuotes loadQuotes, string resource)
    {XmlSerializer serializer = new XmlSerializer(typeof(LoadQuotes));
        using (TextWriter writer = new StreamWriter(resource))
        {
            serializer.Serialize(writer, loadQuotes);
        }
    }

    public static void Deseralize(out LoadQuotes loadQuotes, string resource)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(LoadQuotes));
        TextReader reader = new StreamReader(resource);
        object obj = deserializer.Deserialize(reader);
        loadQuotes = (LoadQuotes)obj;
        reader.Close();
    }

    public void AssignNamesFromFilenames()
    {
        foreach (QuoteContainer container in quotes)
        {
            foreach (Quote quote in container.quotes)
            {
                if (quote?.quoteAudio?.name == null)
                {
                    Debug.LogError("Quote is missing audio file: " + container.unityTitle.ToString() + " | " + quote.quote);
                }
                else
                {
                    quote.audioFilename = @"Quotes\" + quote.quoteAudio.name;
                }
            }
        }
    }

    public void LoadAudioFilesFromnames()
    {
        foreach (QuoteContainer container in quotes)
        {
            foreach (Quote quote in container.quotes)
            {
                AudioClip clip = (AudioClip)Resources.Load(quote.audioFilename);
                
                if (clip != null)
                {
                    quote.quoteAudio = clip;
                }
                else
                {
                    Debug.LogError("Quote is missing audio filename: " + container.unityTitle.ToString() + " | " + quote.quote);
                }
            }
        }
    }
}

