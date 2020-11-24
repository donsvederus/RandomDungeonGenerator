using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public float speed;

    LayerMask obstacleMask;  // assigning layer masks to variable
    Vector2 targetPosition;  // this is where we want to move too  
    Transform playerSprite;  // transform assigned to playerSprite
    float flipX;  // assign float flipX
    bool isMoving;  // assigning false by default to isMoving variable

    void Start() // This happens at the start
    {
        //
        obstacleMask = LayerMask.GetMask("Wall", "Enemy");
        // Get the transform properties
        playerSprite = GetComponentInChildren<SpriteRenderer>().transform;
        // Assigns X transform properties to flipX, which is 1
        // Note:  make sure sprite is facing right to begin, Fip the X in Unity
        flipX = playerSprite.localScale.x;
    }

    void Update() // This happens every frame after start
    {
        // Gets horizontal input, without decimals, so -1 for left, or 1 for right, then Assigns it to horizaontalMovement 
        float horizontalMovement = System.Math.Sign(Input.GetAxisRaw("Horizontal"));
        float verticalMovement = System.Math.Sign(Input.GetAxisRaw("Vertical")); // Get Vertical input
        // Debug.Log("Horizontal: " + horizontalMovement);

        // This makes sure the character is moving in any of the 4 directions
        if(Mathf.Abs(horizontalMovement) > 0 || Mathf.Abs(verticalMovement) > 0) {

            // Flipping the character direction
            // If the horizontalMovement is greater then 0
            if(Mathf.Abs(horizontalMovement) > 0) 
            {
                // This changes the x-transform properties by multiplying flipX with horizontalMovement
                //  1 flipX x 1 horizontalMovement = 1 on the x-transform 
                //  1 flipX x -1 horizontalMovement = -1 on the x-transform 
                playerSprite.localScale = new Vector2(flipX * horizontalMovement, playerSprite.localScale.y);
                // Debug.Log("Horizontal: " + horizontalMovement);
                // Debug.Log("flipX: " + flipX);
            }
            
            // Check to see ifMoving
            if(!isMoving) {

                // Character Movement
                if(Mathf.Abs(horizontalMovement) > 0) 
                {
                    // horizontal movement
                    targetPosition = new Vector2(transform.position.x + horizontalMovement, transform.position.y);
                }
                else if(Mathf.Abs(verticalMovement) > 0) 
                {
                    // vertical movement
                    targetPosition = new Vector2(transform.position.x, transform.position.y + verticalMovement);
                }

                // check to see if player collides into something
                // assigning the size of the hit box
                Vector2 hitSize = Vector2.one * 0.8f;
                Collider2D hitSomething = Physics2D.OverlapBox(
                    targetPosition, 
                    hitSize, 
                    0, 
                    obstacleMask
                );

                // Check to see if player hit anything, if not! then start move coroutine
                if(!hitSomething) 
                {
                    StartCoroutine(GoodMove());
                }
            }

        }
    }

    IEnumerator GoodMove() {
        isMoving = true;
        while(Vector2.Distance(transform.position, targetPosition) > 0.01f) {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                targetPosition, 
                speed * Time.deltaTime
            );
            transform.position = targetPosition;
            yield return null;
            
        }
        
        isMoving = false;
    }
}
