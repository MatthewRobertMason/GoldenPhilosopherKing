using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyThrobber : MonoBehaviour
{

    float initialX;
    public float Speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        initialX = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(new Vector2(Time.deltaTime * Speed, 0));

        if(this.gameObject.transform.position.x > -initialX){
            this.gameObject.transform.position = new Vector2(
                initialX,
                this.gameObject.transform.position.y
            );
        }
    }
}
