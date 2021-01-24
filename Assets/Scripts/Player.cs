using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;

    LayerMask obstacleMask; 
    Vector2 targetPosition;  
    Transform playerSprite;  
    float flipX;  
    bool isMoving;  

    void Start() {
        obstacleMask = LayerMask.GetMask("Wall", "Enemy");
        playerSprite = GetComponentInChildren<SpriteRenderer>().transform;
        flipX = playerSprite.localScale.x;
    }

    void Update() {
        Move();
    }

    void Move() {
        float horizontalMovement = System.Math.Sign(Input.GetAxisRaw("Horizontal"));
        float verticalMovement = System.Math.Sign(Input.GetAxisRaw("Vertical"));


        if (Mathf.Abs(horizontalMovement) > 0 || Mathf.Abs(verticalMovement) > 0)
        {
            if (Mathf.Abs(horizontalMovement) > 0)
            {
                playerSprite.localScale = new Vector2(flipX * horizontalMovement, playerSprite.localScale.y);
            }

            // Check to see ifMoving
            if (!isMoving)
            {
                if (Mathf.Abs(horizontalMovement) > 0)
                {
                    targetPosition = new Vector2(transform.position.x + horizontalMovement, transform.position.y);
                }
                else if (Mathf.Abs(verticalMovement) > 0)
                {
                    targetPosition = new Vector2(transform.position.x, transform.position.y + verticalMovement);
                }

                Vector2 hitSize = Vector2.one * 0.8f;
                Collider2D hitSomething = Physics2D.OverlapBox(
                    targetPosition,
                    hitSize,
                    0,
                    obstacleMask
                );

                if (!hitSomething)
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
