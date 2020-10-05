﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;
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

    public static void Deseralize(out LoadQuotes loadQuotes)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(LoadQuotes));
        
        TextReader reader = new StringReader(xmlFile);

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

    public static string xmlFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
<RootQuotes xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <QuoteContainer>
    <UnityTitle>Plato - Execellence</UnityTitle>
    <Quotes>
      <QuoteAuthor>Plato</QuoteAuthor>
      <Quote>""Excellence is not a gift, but a skill that takes practice. We do not act ‘rightly’ because we are ‘excellent’, in fact we achieve ‘excellence’ by acting ‘rightly.’""</Quote>
      <AudioName>Quotes\Plato_Execellence_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Play-Doh</QuoteAuthor>
      <Quote>""Mediocrity is not a gift, but a skill that takes practice. We do not act ‘goodly’ because we are ‘mediocre’, in fact we achieve ‘mediocrity’ by acting 'alright.’""</Quote>
      <AudioName>Quotes\Plato_Execellence_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Per de doo</QuoteAuthor>
      <Quote>""Electricity is not a gift, but a skull that takes ractice. We do not ask ‘righly’ because we are ‘excellerant’, in fact we acheese ‘excesserless’ by acting ‘ribeler.’""</Quote>
      <AudioName>Quotes\Plato_Execellence_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>T.S Eliot - Every Moment</UnityTitle>
    <Quotes>
      <QuoteAuthor>T.S Eliot</QuoteAuthor>
      <Quote>""Every moment is a fresh beginning""</Quote>
      <AudioName>Quotes\TSEliot_Moment_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>T.S Eliot</QuoteAuthor>
      <Quote>""Every beginning is a fresh moment.""</Quote>
      <AudioName>Quotes\TSEliot_Moment_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>B.S Eliot</QuoteAuthor>
      <Quote>""Every moment is moment.""</Quote>
      <AudioName>Quotes\TSEliot_Moment_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Plutarch - Bad Men</UnityTitle>
    <Quotes>
      <QuoteAuthor>Plutarch</QuoteAuthor>
      <Quote>""Bad men live that they may eat and drink, whereas good men eat and drink that they may live""</Quote>
      <AudioName>Quotes\Plutarch_BadMen_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Pluto the Dog</QuoteAuthor>
      <Quote>""Bad men live that they may eat and drink, whereas good men eat and drink so that they may live""</Quote>
      <AudioName>Quotes\Plutarch_BadMen_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Sailor Pluto</QuoteAuthor>
      <Quote>""Bad men live that they may eat and drink, whereas good men eat and drink bad men""</Quote>
      <AudioName>Quotes\Plutarch_BadMen_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Voltaire - Forbidden</UnityTitle>
    <Quotes>
      <QuoteAuthor>Voltaire</QuoteAuthor>
      <Quote>“It is forbidden to kill; therefore all murderers are punished unless they kill in large numbers and to the sound of trumpets.”</Quote>
      <AudioName>Quotes\Voltaire_forbidden_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Volture</QuoteAuthor>
      <Quote>""It is forbidden to kill; therefore all murderers are punished unless they kill in large numbers or with the use of a trolley""</Quote>
      <AudioName>Quotes\Voltaire_forbidden_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Viletare</QuoteAuthor>
      <Quote>""Killing is forbidden; unless you are a murderer. punishment is to kill in large numbers and with a trumpets""</Quote>
      <AudioName>Quotes\Voltaire_forbidden_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Confucius - The Mind</UnityTitle>
    <Quotes>
      <QuoteAuthor>Confucius</QuoteAuthor>
      <Quote>""The mind of the superior man is conversant with righteousness; the mind of the mean man is conversant with gain.""</Quote>
      <AudioName>Quotes\Confucius_TheMind_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Confuseus</QuoteAuthor>
      <Quote>""The mine of the superior map is conservant with righteous; the mind of the mean map is conservant with grain.""</Quote>
      <AudioName>Quotes\Confucius_TheMind_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Fucondus</QuoteAuthor>
      <Quote>""The mine of the surpreme man is covalesent with ripe oranges; the mine of the meat man is conditionaing grain.""</Quote>
      <AudioName>Quotes\Confucius_TheMind_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Ernest Hemingway - About Morals</UnityTitle>
    <Quotes>
      <QuoteAuthor>Ernest Hemingway</QuoteAuthor>
      <Quote>""About morals, I know only that what is moral is what you feel good after and what is immoral is what you feel bad after.""</Quote>
      <AudioName>Quotes\Hemingway_AboutMorals_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Erny Hem-Away</QuoteAuthor>
      <Quote>""About morals, I know only that what is moral is what you feel good after and what is immoral is what you feel many times more good after.""</Quote>
      <AudioName>Quotes\Hemingway_AboutMorals_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Ernthulu</QuoteAuthor>
      <Quote>""About mortals, I know that what is mortal is what you feed goo too and what is immortal is what you retrieve the goo from.""</Quote>
      <AudioName>Quotes\Hemingway_AboutMorals_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Marcus Aurelius - Waste</UnityTitle>
    <Quotes>
      <QuoteAuthor>Marcus Aurelius</QuoteAuthor>
      <Quote>""Waste no more time arguing about what a good man should be. Be one.""</Quote>
      <AudioName>Quotes\MarcusAurelius_Waste_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Marc Aurelius</QuoteAuthor>
      <Quote>""Wait no more time arguing about what a good man shouldn't be. Be one.""</Quote>
      <AudioName>Quotes\MarcusAurelius_Waste_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Marc Marc Marc Marc Marc</QuoteAuthor>
      <Quote>""Waste no more time aruguling about what what man a good should be. Be meat.""</Quote>
      <AudioName>Quotes\MarcusAurelius_Waste_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Robert Oppenheimer - Become Death</UnityTitle>
    <Quotes>
      <QuoteAuthor>Robert Oppenheimer</QuoteAuthor>
      <Quote>""Now I am become Death, the destroyer of worlds""</Quote>
      <AudioName>Quotes\Oppenheimer_NowIAm_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Robbie Oldentimer</QuoteAuthor>
      <Quote>""Now I am become Deaf, the destructer of worlds""</Quote>
      <AudioName>Quotes\Oppenheimer_NowIAm_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Offelater</QuoteAuthor>
      <Quote>""Now am Deaf world become destroy""</Quote>
      <AudioName>Quotes\Oppenheimer_NowIAm_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Aristotle - Happiness</UnityTitle>
    <Quotes>
      <QuoteAuthor>Aristotle</QuoteAuthor>
      <Quote>""Happiness depends upon ourselves.""</Quote>
      <AudioName>Quotes\Aristotle_Happiness_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Ari Totle</QuoteAuthor>
      <Quote>""Happiness depends upon arseholes.""</Quote>
      <AudioName>Quotes\Aristotle_Happiness_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Totle</QuoteAuthor>
      <Quote>""Happiness depends upon happiness depends upon happiness depends upon happiness depends upon happiness.""</Quote>
      <AudioName>Quotes\Aristotle_Happiness_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>John Rawls - Justice</UnityTitle>
    <Quotes>
      <QuoteAuthor>John Rawls, A Theory of Justice </QuoteAuthor>
      <Quote>“Justice is the first virtue of social institutions, as truth is of systems of thought.”</Quote>
      <AudioName>Quotes\Rawls_Justice_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>John Rawls, A Theory of Juice</QuoteAuthor>
      <Quote>""Juice is the first virtue of social institutions, as thirst is of systems of thought.”</Quote>
      <AudioName>Quotes\Rawls_Justice_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>John ""The Juice"" Rawls, A Theory of Juice</QuoteAuthor>
      <Quote>“Juice is the first virtue of institutions, as thirst is of systems.”</Quote>
      <AudioName>Quotes\Rawls_Justice_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>John Rawls - Prosper</UnityTitle>
    <Quotes>
      <QuoteAuthor>John Rawls, A Theory of Justice</QuoteAuthor>
      <Quote>“It may be expedient but it is not just that some should have less in order that others may prosper.”</Quote>
      <AudioName>Quotes\Rawls_Prosper_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>John Rawls, A Theory of Rawls</QuoteAuthor>
      <Quote>“It may be expedient but it is not cool that some should have less in order that others may be rad.” </Quote>
      <AudioName>Quotes\Rawls_Prosper_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>J-Rawl</QuoteAuthor>
      <Quote>“It is not cool that some should suck it in order that others may be rad.”</Quote>
      <AudioName>Quotes\Rawls_Prosper_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Plato - Wealth</UnityTitle>
    <Quotes>
      <QuoteAuthor>Plato</QuoteAuthor>
      <Quote>""The greatest wealth is to live content with little.""</Quote>
      <AudioName>Quotes\Plato_Wealth_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Potato</QuoteAuthor>
      <Quote>""The greatest wealth is to live content with wealth.""</Quote>
      <AudioName>Quotes\Plato_Wealth_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Potato</QuoteAuthor>
      <Quote>""The greatest wealth is potato.""</Quote>
      <AudioName>Quotes\Plato_Wealth_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Plato - Forgive</UnityTitle>
    <Quotes>
      <QuoteAuthor>Plato</QuoteAuthor>
      <Quote>""We can easily forgive a child who is afraid of the dark; the real tragedy of life is when men are afraid of the light.""</Quote>
      <AudioName>Quotes\Plato_Forgive_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Plato</QuoteAuthor>
      <Quote>""We can easily forgive a child who is afraid of the dark; the real tragedy is when grown men are also afraid of the dark.""</Quote>
      <AudioName>Quotes\Plato_Forgive_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Playdough</QuoteAuthor>
      <Quote>""We can easily forgive a child who is afraid of the dark; but you can still laugh at them.""</Quote>
      <AudioName>Quotes\Plato_Forgive_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Democritus - Greed</UnityTitle>
    <Quotes>
      <QuoteAuthor>Democritus</QuoteAuthor>
      <Quote>""It is greed to do all the talking but not to want to listen at all.""</Quote>
      <AudioName>Quotes\Democritus_Greed_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Demo Citrus</QuoteAuthor>
      <Quote>""It is greed to do all the talking but great to not listen at all.""</Quote>
      <AudioName>Quotes\Democritus_Greed_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Demo Cee</QuoteAuthor>
      <Quote>""Greed is all the talking but listen, listen to it all.""</Quote>
      <AudioName>Quotes\Democritus_Greed_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Democritus - Strive</UnityTitle>
    <Quotes>
      <QuoteAuthor>Democritus</QuoteAuthor>
      <Quote>""Men should strive to think much and know little.""</Quote>
      <AudioName>Quotes\Democritus_Strive_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Critus</QuoteAuthor>
      <Quote>""Men should strive to think much and know more.""</Quote>
      <AudioName>Quotes\Democritus_Strive_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Boss Lemons</QuoteAuthor>
      <Quote>""Knowing and thinking is little, strive to be bigly.""</Quote>
      <AudioName>Quotes\Democritus_Strive_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
  <QuoteContainer>
    <UnityTitle>Voltaire- Considerate</UnityTitle>
    <Quotes>
      <QuoteAuthor>Voltaire</QuoteAuthor>
      <Quote>""We should be considerate to the living, to the dead we owe only the truth""</Quote>
      <AudioName>Quotes\Voltaire_Considerate_0</AudioName>
      <DistortionLevel>0</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Voltaire</QuoteAuthor>
      <Quote>""We should be considerate to the living, to the dead we owe only 5 bucks""</Quote>
      <AudioName>Quotes\Voltaire_Considerate_1</AudioName>
      <DistortionLevel>1</DistortionLevel>
    </Quotes>
    <Quotes>
      <QuoteAuthor>Voltaire</QuoteAuthor>
      <Quote>""We should be considerate to the dead, they're dead, it's really hard for them""</Quote>
      <AudioName>Quotes\Voltaire_Considerate_2</AudioName>
      <DistortionLevel>2</DistortionLevel>
    </Quotes>
  </QuoteContainer>
</RootQuotes>
";
}

