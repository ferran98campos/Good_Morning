using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{   
    public float movementSpeed = 1f;
    public float collisionOffset = 0.05f; 
    public ContactFilter2D  movementFilter; 
    Vector2 movementInput; 
    Rigidbody2D rb; 
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    SpriteRenderer sr; 
    Animator animator; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();  
        animator = GetComponent<Animator>(); 
    }

    void Update() {
        //Making sure that animations catch up 
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        //Naive way of making sure that player faces the same direction when going idle
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical")); 
        }

    }

    void FixedUpdate() {

        if(movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

            //If two perpendicular keys are pressed and only one leads to collision, 
            //we enable movement in the other direction
            if(!success) {
                success = TryMove(new Vector2(movementInput.x, 0));

                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y)); 
                }
            }
        }
    }

    //Casts a ray and sees if making this move would result in collision. 
    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction, //Vec2 corresponding to direction relative to current position
            movementFilter,
            castCollisions, 
            movementSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count == 0) {
            //Ray does not collide
            rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
            return true;  
        } else {
            return false; 
        }


    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>(); 
    }
}
