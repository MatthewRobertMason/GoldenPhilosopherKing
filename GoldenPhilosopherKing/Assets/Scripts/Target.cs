using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private SpriteRenderer sprite;
    public Vector2 escapeVector;

    void Awake(){
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other){
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(escapeVector);
    }

    public void SetSprite(Sprite image){
        sprite.sprite = image;
        UpdatePolygonCollider2D();
    }

    // Store these outside the method so it can reuse the Lists (free performance)
     private List<Vector2> _points = new List<Vector2>();
     private List<Vector2> _simplifiedPoints = new List<Vector2>();
     public void UpdatePolygonCollider2D(float tolerance = 0.05f)
     {
         var polygonCollider2D = this.gameObject.GetComponent<PolygonCollider2D>();
         if(polygonCollider2D.enabled){
            polygonCollider2D.pathCount = sprite.sprite.GetPhysicsShapeCount();
            for(int i = 0; i < polygonCollider2D.pathCount; i++)
            {
                sprite.sprite.GetPhysicsShape(i, _points);
                LineUtility.Simplify(_points, tolerance, _simplifiedPoints);
                polygonCollider2D.SetPath(i, _simplifiedPoints);
            }
         }
     }
}
