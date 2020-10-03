using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TrackSegment {
    Entering, BottomRight, ExitRight, Top, ExitLeft, BottomLeft
}

public class TrolleyDriver : MonoBehaviour
{

    public Sprite MovingUp;
    public Sprite MovingRight;
    public Sprite MovingDown;
    public Sprite MovingLeft;

    public Transform EnterSegment;
    public Transform ExitRightSegment;
    public Transform ExitLeftSegment;
    public Transform TopSegment;
    public Transform BottomRightSegment;
    public Transform BottomLeftSegment;

    public bool ExitRight = false;
    public bool ExitLeft = false;

    private TrackSegment currentSegment = TrackSegment.Entering;
    private Transform currentPathObject = null;
    private float segmentDistance = 0;
    private float segmentSpeed = 1;

    public GameObject Trolley;

    void Start(){
        currentPathObject = EnterSegment;
    }

    // Update is called once per frame
    void Update()
    {
        segmentDistance += Time.deltaTime * segmentSpeed;
        if(segmentDistance >= 1){
            switch(currentSegment){
                case TrackSegment.Entering:
                case TrackSegment.BottomLeft:
                    segmentSpeed = 0.6f;
                    currentSegment = TrackSegment.BottomRight;
                    currentPathObject = BottomRightSegment;
                    break;

                case TrackSegment.BottomRight:
                    if(ExitRight){
                        segmentSpeed = 0.5f;
                        currentSegment = TrackSegment.ExitRight;
                        currentPathObject = ExitRightSegment;
                    } else {
                        segmentSpeed = 0.3f;
                        currentSegment = TrackSegment.Top;
                        currentPathObject = TopSegment;
                    }
                    break;

                case TrackSegment.Top:
                    if(ExitLeft){
                        segmentSpeed = 0.5f;
                        currentSegment = TrackSegment.ExitLeft;
                        currentPathObject = ExitLeftSegment;
                    } else {
                        segmentSpeed = 0.4f;
                        currentSegment = TrackSegment.BottomLeft;
                        currentPathObject = BottomLeftSegment;
                    }
                    break;

                case TrackSegment.ExitLeft: 
                case TrackSegment.ExitRight:
                    Session.Current.Reset();
                    return;
            }
            segmentDistance = 0;
        }

        DrawTrolley();
    }

    void DrawTrolley(){
        Vector2 p0 = currentPathObject.GetChild(0).position;
        Vector2 p1 = currentPathObject.GetChild(1).position;
        Vector2 p2 = currentPathObject.GetChild(2).position;
        Vector2 p3 = currentPathObject.GetChild(3).position;

        Vector2 oldPosition = Trolley.transform.position;

        Trolley.transform.position = Mathf.Pow(1 - segmentDistance, 3) * p0 + 
            3 * Mathf.Pow(1 - segmentDistance, 2) * segmentDistance * p1 + 
            3 * (1 - segmentDistance) * Mathf.Pow(segmentDistance, 2) * p2 + 
            Mathf.Pow(segmentDistance, 3) * p3;

        Vector2 delta = (Vector2)Trolley.transform.position - oldPosition;
        if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y)){
            if(delta.x > 0){
                DrawMovingRight();
            } else {
                DrawMovingLeft();
            }
        } else {
            if(delta.y > 0){
                DrawMovingUp();
            } else {
                DrawMovingDown();
            }
        }
    }

    void DrawMovingUp(){
        Trolley.GetComponent<SpriteRenderer>().sprite = MovingUp;
        Trolley.GetComponent<SpriteRenderer>().flipX = false;
    }

    void DrawMovingRight(){
        Trolley.GetComponent<SpriteRenderer>().sprite = MovingRight;        
        Trolley.GetComponent<SpriteRenderer>().flipX = false;
    }

    void DrawMovingDown(){
        Trolley.GetComponent<SpriteRenderer>().sprite = MovingDown;        
        Trolley.GetComponent<SpriteRenderer>().flipX = false;
    }

    void DrawMovingLeft(){
        Trolley.GetComponent<SpriteRenderer>().sprite = MovingLeft;
        Trolley.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void TurnRight(){
        ExitRight = true;
        ExitLeft = false;
    }
    public void TurnLeft(){
        ExitLeft = true;
        ExitRight = false;
    }

}
