using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour
{
    public static Session Current;

    // Start is called before the first frame update
    void Start()
    {
        if(Current){
            Destroy(this.gameObject);
        } else {
            Current = this;
            Object.DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Reset(){
        SceneManager.LoadScene("GameBoard");
    }
}
