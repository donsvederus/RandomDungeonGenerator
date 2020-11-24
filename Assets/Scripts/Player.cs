using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    // transform assigned to playerSprite
    Transform playerSprite;
    // assign float flipX
    float flipX;

    void Start() // This happens at the start
    {
        // Get the transform properties
        playerSprite = GetComponentInChildren<SpriteRenderer>().transform;
        // Assigns X transform properties to flipX, which is 1
        // Note:  make sure sprite is facing right to begin, Fip the X in Unity
        flipX = playerSprite.localScale.x;
    }

    void Update() // This happens every frame after start
    {
        // Gets the horizontal input, without decimals, so -1 for left, or 1 for right, then Assigns it to horizaontalMovement 
        float horizontalMovement = System.Math.Sign(Input.GetAxisRaw("Horizontal"));
        // Debug.Log("Horizontal: " + horizontalMovement);

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
        
    }
}
