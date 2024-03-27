using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;
    public bool canDoubleJump;

    public AudioClip JumpSoundEffect;
    private AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        //assign the audiosource variable to allow all sound effects
        audioSource = GetComponent<AudioSource>();

        //flip character
        input = Input.GetAxisRaw("Horizontal");
        if(input < 0 )
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0 ) 
        {
            spriteRenderer.flipX = false;
        }
        //assigning what isGrounded means
        //creates the circle at players feet to detect collision with ground
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        //if player is grounded and presses jump button they will jump once
        //uses jump counter so you cant float in the air indefinately
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;
            canDoubleJump = true;
            audioSource.PlayOneShot(JumpSoundEffect);
        }
        else if (canDoubleJump == true && Input.GetButtonDown("Jump"))
        {
            playerRB.velocity= Vector2.up * jumpForce;
            canDoubleJump = false;
            audioSource.PlayOneShot(JumpSoundEffect);


        }


        //stops jumping when spacebar is released
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        

    }

    void FixedUpdate()
    {
        //moves left to right
        playerRB.velocity = new Vector2 (input * speed, playerRB.velocity.y);
    }
}
