using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        //assign the audiosource variable to allow all sound effects
        audioSource = GetComponent<AudioSource>();

        //set speed in animator to control animations
        animator.SetFloat("Speed", Mathf.Abs(input));

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
        if (isGrounded == true && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;
            canDoubleJump = true;
            audioSource.PlayOneShot(JumpSoundEffect);
            animator.SetBool("isJumping", true);
        }
        else if (canDoubleJump == true && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            
            playerRB.velocity= Vector2.up * jumpForce;
            canDoubleJump = false;
            audioSource.PlayOneShot(JumpSoundEffect);
            animator.SetBool("isJumping", true);


        }

        //on ground
        if (isGrounded == true && Mathf.Abs(playerRB.velocity.y) < 0.001f) 
        {
            animator.SetBool("isJumping", false);
        }


        //stops jumping when spacebar is released
        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }

        //when level increases, inc speed by 10
        if (LevelTracker.currentLevel>LevelTracker.previousLevel)
        {
            increaseSpeedBy10();
        }






    }

    void FixedUpdate()
    {
        //moves left to right
        playerRB.velocity = new Vector2 (input * speed, playerRB.velocity.y);
    }


    private void increaseSpeedBy10()
    {
        speed += 10;
    }    
}
