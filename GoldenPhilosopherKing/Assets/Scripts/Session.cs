using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    public static Session Current;
    public Sprite[] TargetSprites = new Sprite[0];

    public TextAsset QuoteFile; 
    private List<string> quotes = new List<string>();
    private List<string> quoteAuthors = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if(Current){
            Destroy(this.gameObject);
        } else {
            Current = this;
            Object.DontDestroyOnLoad(this.gameObject);

            bool onQuote = true;
            foreach(string rawLine in QuoteFile.text.Split('\n')){
                string line = rawLine.Trim();
                if(line.Length == 0) continue;
                if(onQuote){
                    quotes.Add(line);
                } else {
                    quoteAuthors.Add(line);
                }
                onQuote = !onQuote;
            }
        }
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
        int quoteIndex = Random.Range(0, quotes.Count);
        GameObject.Find("QuoteTextBox").GetComponent<Text>().text = quotes[quoteIndex];
        GameObject.Find("AttributionTextBox").GetComponent<Text>().text = quoteAuthors[quoteIndex];
    }
}
