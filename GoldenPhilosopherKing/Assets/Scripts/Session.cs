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
            Debug.Log("Only one session object should be set.");
            GameObject.Destroy(this);
        }
        Current = this;
    }

    public void Reset(){
        SceneManager.LoadScene("GameBoard");
    }
}
