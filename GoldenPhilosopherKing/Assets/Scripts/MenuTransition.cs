using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    private float transitionElapsed = 0;
    private bool moving = true;
    public float TransitionDuration = 3;
    private Vector2 transitionStart;

    // Start is called before the first frame update
    void Start()
    {
        transitionStart = this.gameObject.transform.position;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            transitionElapsed += Time.deltaTime;
            float t = transitionElapsed/TransitionDuration;

            if(t < 1){
                var path = new Vector2(0, 0) - transitionStart;

                var fuzz = 2 * t * (1 - t);

                this.gameObject.transform.position = transitionStart + path * (t + fuzz);

            } else {
                this.gameObject.transform.position = new Vector2(0, 0);
                moving = false;
            }
        }
    }
}
