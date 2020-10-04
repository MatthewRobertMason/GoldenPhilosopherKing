using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class TargetSet {
    public Sprite sprite;
    public Sprite sign;
};

public class Session : MonoBehaviour
{
    public static Session Current;
    public Sprite[] TargetSprites = new Sprite[0];
    public TargetSet[] Targets = new TargetSet[0];

    public LoadQuotes loadedQuotes;

    private AudioManager audioManager;
    private VoiceManager voiceManager;

    public string quotesXMLFile = @"Assets\Voice\QuoteData.xml";

    public int currentLevel = 0;

    public bool PlayingVoice
    {
        get { return voiceManager.IsPlaying; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Current){
            Destroy(this.gameObject);
        } else {
            Current = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }

        audioManager = FindObjectOfType<AudioManager>();
        voiceManager = FindObjectOfType<VoiceManager>();

        LoadQuotes.Deseralize(out loadedQuotes, quotesXMLFile);
        loadedQuotes.LoadAudioFilesFromnames();

        Current.currentLevel += 1;
        Current.SceneStart();
    }

    private void FixedUpdate()
    {
        if (loadedQuotes.saveDataToFile)
        {
            loadedQuotes.AssignNamesFromFilenames();
            LoadQuotes.Serialize(loadedQuotes, quotesXMLFile);

            loadedQuotes.saveDataToFile = false;
        }
    }

    public void Reset(){
        SceneManager.LoadScene("GameBoard");
    }

    void SceneStart(){
        // Add two random target sprites
        int first = Random.Range(0, TargetSprites.Length);
        int second = -1;
        while(second == -1 || second == first){
            second = Random.Range(0, TargetSprites.Length);
        }

        var targets = FindObjectsOfType<Target>();
        if(targets.Length > 0) targets[0].SetSprite(TargetSprites[first]);
        if(targets.Length > 1) targets[1].SetSprite(TargetSprites[second]);

        // Set a random quote text
        int quoteIndex = Random.Range(0, loadedQuotes.quotes.Length);
        int distortionLevel = Random.Range(0, 3);

        Quote quote = loadedQuotes.quotes[quoteIndex].GetQuote(distortionLevel);
        Quote quote0 = loadedQuotes.quotes[quoteIndex].GetQuote(0);
        GameObject.Find("QuoteTextBox").GetComponent<Text>().text = quote0.quote; //quote.quote;
        GameObject.Find("AttributionTextBox").GetComponent<Text>().text = quote0.quoteAuthor;

        voiceManager.PlayVoice(quote.quoteAudio);
        audioManager.pitchVariance = 
            Mathf.Clamp(
                (currentLevel-audioManager.levelDelayUntilPitchWalk) * audioManager.pitchVariancePerLevel, 
                0.0f, 
                audioManager.maxPitchVariance);

        // Set a random title
        GameObject.Find("TitleTextBox").GetComponent<Text>().text = GenerateMoralAlignment();        
    }

    string GenerateMoralAlignment(){
        string result = "";
        if(Random.Range(0.0f, 1.0f) < 0.8f){
            string[] prefixes = {
                "Bronze",
                "Pewter",
                "Putty",
                "Wooden",
                "Profound",
                "Hollow",
                "Goblin"
            };
            result += prefixes[Random.Range(0, prefixes.Length)] + " ";
        }

        string[] role = {
            "Bandit",
            "Haberdasher",
            "Student",
            "Sage",
            "Clerk",
            "Drunk"
        };

        result += role[Random.Range(0, role.Length)];

        return result;
    }
}
