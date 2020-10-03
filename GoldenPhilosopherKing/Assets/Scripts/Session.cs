using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    public static Session Current;
    public Sprite[] TargetSprites = new Sprite[0];

    public Quote[] quotes;

    private AudioManager audioManager;
    private VoiceManager voiceManager;

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

        Current.SceneStart();
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
        int quoteIndex = Random.Range(0, quotes.Length);
        GameObject.Find("QuoteTextBox").GetComponent<Text>().text = quotes[quoteIndex].quote;
        GameObject.Find("AttributionTextBox").GetComponent<Text>().text = quotes[quoteIndex].quoteAuthor;

        voiceManager.PlayVoice(quotes[quoteIndex].quoteAudio);

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
