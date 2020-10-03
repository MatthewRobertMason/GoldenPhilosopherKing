using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour
{
    public static Session Current;
    public Sprite[] TargetSprites = new Sprite[0];

    // Start is called before the first frame update
    void Start()
    {
        if(Current){
            Destroy(this.gameObject);
        } else {
            Current = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }
        Current.SceneStart();
    }

    public void Reset(){
        SceneManager.LoadScene("GameBoard");
    }

    void SceneStart(){
        int first = Random.Range(0, TargetSprites.Length);
        int second = -1;
        while(second == -1 || second == first){
            second = Random.Range(0, TargetSprites.Length);
        }

        var targets = FindObjectsOfType<Target>();
        if(targets.Length > 0) targets[0].SetSprite(TargetSprites[first]);
        if(targets.Length > 1) targets[1].SetSprite(TargetSprites[second]);
    }
}
