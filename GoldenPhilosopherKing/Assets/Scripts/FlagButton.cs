using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagButton : MonoBehaviour
{
    public TrolleyDriver Trolley;
    public bool TurnLeft = true;

    void OnMouseDown(){
        if(TurnLeft) Trolley.TurnLeft();
        else Trolley.TurnRight();
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
