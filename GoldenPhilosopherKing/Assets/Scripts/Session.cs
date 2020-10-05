using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class TargetSet {
    public Sprite sprite;
    public Sprite sign;
};

[System.Serializable]
public class IntermissionSegment {
    public int levelIndex;
    public AudioClip segmentAudio;
};

public class Session : MonoBehaviour
{
    public static Session Current;
    public TargetSet[] Targets = new TargetSet[0];
    public IntermissionSegment[] Intermissions = new IntermissionSegment[0];
    public AudioClip[] randomIntermissionAudio;
    public AudioClip defaultAudioClip;
    private string transitionAfterPlay = null;
    public LoadQuotes loadedQuotes;

    private AudioManager audioManager;
    private VoiceManager voiceManager;
    private OptionsManager optionsManager;
    public GameObject optionsButton;

    public string quotesXMLFile = @"Assets\Voice\QuoteData.xml";
    public string resourcesQuotesXMLFile = @"QuoteData.xml";

    public TextAsset xmlFile;

    private Queue<int> previousQuotes;
    public int currentLevel = 0;

    public bool seeActualQuotes = false;

    public bool PlayingVoice
    {
        get { return voiceManager.IsPlaying; }
    }

    private void Awake()
    {
        previousQuotes = new Queue<int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Current){
            Destroy(this.gameObject);
        } else {
            Current = this;
            Object.DontDestroyOnLoad(this.gameObject);
            
            audioManager = FindObjectOfType<AudioManager>();
            voiceManager = FindObjectOfType<VoiceManager>();
            optionsManager = FindObjectOfType<OptionsManager>();

            
            //LoadQuotes.Deseralize(out loadedQuotes, resourcesQuotesXMLFile);
            LoadQuotes.Deseralize(out loadedQuotes);
            //Debug.Log("loaded resourceFile Quotes");
            
            loadedQuotes.LoadAudioFilesFromnames();
        }

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

    public void Update()
    {
        OptionsUpdates();
        if(transitionAfterPlay != null){
            if(!voiceManager.IsPlaying){
                var dest = transitionAfterPlay;
                transitionAfterPlay = null;
                SceneManager.LoadScene(dest);        
            }
        }
    }

    public void Reset(){
        SceneManager.LoadScene("Intermission");
    }

    void SceneStart(){
        var name = SceneManager.GetActiveScene().name;
        if(name == "Intermission"){
            transitionAfterPlay = "GameBoard";
            
            if (Intermissions.Where(p => p.levelIndex == currentLevel)?.FirstOrDefault() != null)
            {
                voiceManager.PlayVoice(Intermissions.Where(p => p.levelIndex == currentLevel).FirstOrDefault().segmentAudio);
            }
            else if (this.currentLevel >= 50)
            {
                if (defaultAudioClip != null)
                {
                    voiceManager.PlayVoice(defaultAudioClip);
                }
            }
            else
            {
                if (randomIntermissionAudio.Count() > 0)
                {
                    int a = Random.Range(0, randomIntermissionAudio.Count());

                    voiceManager.PlayVoice(randomIntermissionAudio[a]);
                }
                else
                {
                    if (defaultAudioClip != null)
                    {
                        voiceManager.PlayVoice(defaultAudioClip);
                    }
                }
            }

            optionsButton.SetActive(false);
        } else if(name == "GameBoard"){
            this.currentLevel += 1;
            LevelStart();
            optionsButton.SetActive(true);
        }
    }

    void LevelStart(){

        // Add two random target sprites
        int first = Random.Range(0, Targets.Length);
        int second = -1;
        while(second == -1 || second == first){
            second = Random.Range(0, Targets.Length);
        }

        GameObject.Find("LeftTarget").GetComponent<Target>().SetSprite(Targets[first].sprite);
        GameObject.Find("LeftTargetSign").GetComponent<SpriteRenderer>().sprite = Targets[first].sign;
        GameObject.Find("RightTarget").GetComponent<Target>().SetSprite(Targets[second].sprite);
        GameObject.Find("RightTargetSign").GetComponent<SpriteRenderer>().sprite = Targets[second].sign;

        // Set a random quote text
        int quoteIndex = -1;

        do
        {
            quoteIndex = Random.Range(0, loadedQuotes.quotes.Length);
        } while (previousQuotes.Contains(quoteIndex));


        int distortionLevel = Random.Range(0, 3);

        Quote quote = loadedQuotes.quotes[quoteIndex].GetQuote(distortionLevel);
        Quote quote0 = loadedQuotes.quotes[quoteIndex].GetQuote(0);

        if (seeActualQuotes)
        {
            GameObject.Find("QuoteTextBox").GetComponent<Text>().text = quote.quote;
            GameObject.Find("AttributionTextBox").GetComponent<Text>().text = quote.quoteAuthor;
        }
        else
        {
            GameObject.Find("QuoteTextBox").GetComponent<Text>().text = quote0.quote;
            GameObject.Find("AttributionTextBox").GetComponent<Text>().text = quote0.quoteAuthor;
        }

        if (previousQuotes.Count < 10)
        {
            previousQuotes.Enqueue(quoteIndex);
        }
        else
        {
            previousQuotes.Dequeue();
            previousQuotes.Enqueue(quoteIndex);
        }

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

    private void OptionsUpdates()
    {
        if (optionsManager.AudioMute)
        {
            audioManager.volume = 0.0f;
        }
        else
        {
            audioManager.volume = optionsManager.AudioVolume;
        }

        if (optionsManager.VoiceMute)
        {
            voiceManager.volume = 0.0f;
            voiceManager.audioMultiplierWhenPlaying = 1.0f;
        }
        else
        {
            voiceManager.volume = optionsManager.VoiceVolume;
            voiceManager.audioMultiplierWhenPlaying = optionsManager.AudioReductionWhenVoicePlaying;
        }

        // Secret options
        bool blur = optionsManager.Blur;
        audioManager.enablePitchWalk = optionsManager.PitchWarp;
        
        // Super Secret Options
        seeActualQuotes = optionsManager.SeeActualQuotes;
    }
}
