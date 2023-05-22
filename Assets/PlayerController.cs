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
    SoundEffects sfx; 
    public float FootStepDelay = 0;
    private ObjectController objectController; 
    private bool walkOnRock = false; 
    private AreaEffector2D currentField; 
    

    public Transform torchTransform;
    bool torchOn;

    // For detecting a layer
    [SerializeField] private LayerMask layerMask;
    private bool isSwimming = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();  
        animator = GetComponent<Animator>();
        sfx = GetComponent<SoundEffects>(); 
        animator = GetComponent<Animator>(); 
        objectController = GetComponent<ObjectController>(); 

        //Torch
        torchOn = true;
        torchTransform = transform.Find("Torch_DirectionalLight").transform;

    }

    void Update() {
        // For detecting whether in the river
        bool isTouchingWater = Physics2D.OverlapCircle(GetComponent<Collider2D>().bounds.center, 0.2f, layerMask);
        if (isTouchingWater && !isSwimming && !walkOnRock)
        {
            isSwimming = true;
            animator.SetBool("IsSwimming", true);
        }
        else if (!isTouchingWater && isSwimming || walkOnRock)
        {
            isSwimming = false;
            animator.SetBool("IsSwimming", false);
        }

        //Making sure that animations catch up 
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
        if(movementInput.sqrMagnitude > .1f) {
            objectController.Direction = movementInput.normalized; 
        }

        //Naive way of making sure that player faces the same direction when going idle
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical")); 
        }

        //Update Torch Transform
        if(torchOn){
            TorchPositionController(torchTransform);
        }
    }

    void FixedUpdate() {

        // If no key is pressed, remain in current position.
        if(movementInput == Vector2.zero) {
            rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);
        }

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
            //Play footstep 
            if(!sfx.IsRunning() && !isSwimming) 
                StartCoroutine(sfx.PlayFootStep(FootStepDelay));
            return true;  
        } else {
            return false; 
        }


    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>(); 
    }

    void TorchPositionController(Transform torchTransform){

        //Rotate
        float angle = torchTransform.rotation.eulerAngles.z;
        if (Input.GetKeyDown("left"))
        {
            angle = 90.0f;
        }else if (Input.GetKeyDown("up")){
            angle = 0.0f;
        }else if (Input.GetKeyDown("right")){
            angle = 270f;
        }else if (Input.GetKeyDown("down")){
            angle = 180.0f;
        }

        torchTransform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Throwable_Rock") {
            walkOnRock = true;
            if(currentField != null)
                currentField.enabled = false;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            currentField = other.gameObject.GetComponent<AreaEffector2D>();
            if(walkOnRock) {
                //Player is walking on a rock, disable areaaffector
                currentField = other.gameObject.GetComponent<AreaEffector2D>();
                currentField.enabled = false; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Throwable_Rock") {
            walkOnRock = false; 
            if(currentField != null)
                currentField.enabled = true;
            
        }
    }
}
