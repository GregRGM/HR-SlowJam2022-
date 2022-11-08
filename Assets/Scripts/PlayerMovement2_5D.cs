using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2_5D : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector3 deathKick = new Vector3 (10f, 10f, 0f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    
    Vector2 moveInput;
    Rigidbody myRigidbody;
    Animator myAnimator;
    CapsuleCollider myBodyCollider;
    BoxCollider myFeetCollider;
    float gravityScaleAtStart;

    bool isGrounded = true;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider>();
        myFeetCollider = GetComponent<BoxCollider>();
        //gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        //ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        //if (!myFeetCollider.layer(LayerMask.GetMask("Ground"))) { return;}
        
        if(value.isPressed)
        {
            // do stuff
            myRigidbody.velocity += new Vector3 (0f, jumpSpeed, 0f);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    //void ClimbLadder()
    //{
    //    if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
    //    { 
    //        myRigidbody.gravityScale = gravityScaleAtStart;
    //        myAnimator.SetBool("isClimbing", false);
    //        return;
    //    }
        
    //    Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
    //    myRigidbody.velocity = climbVelocity;
    //    myRigidbody.gravityScale = 0f;

    //    bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    //    myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    //}

    void Die()
    {
        //if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        //{
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            //FindObjectOfType<GameSession>().ProcessPlayerDeath();
        //}
    }

}
