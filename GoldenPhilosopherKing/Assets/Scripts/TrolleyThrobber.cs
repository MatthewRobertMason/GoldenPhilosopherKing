using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyThrobber : MonoBehaviour
{

    float initialX;
    private float length = -1;

    // Start is called before the first frame update
    void Start()
    {
        initialX = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var voice = GameObject.FindObjectOfType<VoiceManager>();
        if(voice && voice.IsPlaying){
            length = voice.ClipLength();
            if(length == -1){
                length = 10;
            }
        } else {
            length = 10;
        }

        float velocity = (Mathf.Abs(initialX)*2)/length;

        this.gameObject.transform.Translate(new Vector2(Time.deltaTime * velocity, 0));

        if(this.gameObject.transform.position.x > -initialX){
            this.gameObject.transform.position = new Vector2(
                initialX,
                this.gameObject.transform.position.y
            );
        }
    }
}
