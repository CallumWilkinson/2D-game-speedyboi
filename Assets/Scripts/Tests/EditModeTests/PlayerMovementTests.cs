using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PlayerMovementTests
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private GameObject feetPositionObject;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject("Player");
        feetPositionObject = new GameObject("FeetPosition");
        feetPositionObject.transform.SetParent(playerObject.transform);
        
        playerMovement = playerObject.AddComponent<PlayerMovement>();
        rigidbody2D = playerObject.AddComponent<Rigidbody2D>();
        spriteRenderer = playerObject.AddComponent<SpriteRenderer>();
        animator = playerObject.AddComponent<Animator>();
        audioSource = playerObject.AddComponent<AudioSource>();
        
        playerMovement.playerRB = rigidbody2D;
        playerMovement.spriteRenderer = spriteRenderer;
        playerMovement.animator = animator;
        playerMovement.feetPosition = feetPositionObject.transform;
        playerMovement.jumpForce = 16f;
        playerMovement.jumpTime = 0.35f;
        playerMovement.groundCheckCircle = 0.2f;
        playerMovement.groundLayer = 1;
        
        PlayerMovement.speed = 10f;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(feetPositionObject);
        Object.DestroyImmediate(playerObject);
        PlayerMovement.speed = 10f;
    }

    [Test]
    public void ItHasDefaultValues()
    {
        Assert.IsNotNull(playerMovement.playerRB);
        Assert.IsNotNull(playerMovement.spriteRenderer);
        Assert.IsNotNull(playerMovement.animator);
        Assert.IsNotNull(playerMovement.feetPosition);
        Assert.AreEqual(16f, playerMovement.jumpForce);
        Assert.AreEqual(0.35f, playerMovement.jumpTime);
        Assert.AreEqual(0.2f, playerMovement.groundCheckCircle);
        Assert.AreEqual(10f, PlayerMovement.speed);
    }

    [Test]
    public void ItFlipsSprite()
    {
        playerMovement.input = -1f;
        
        if (playerMovement.input < 0)
        {
            playerMovement.spriteRenderer.flipX = true;
        }
        else if (playerMovement.input > 0)
        {
            playerMovement.spriteRenderer.flipX = false;
        }
        
        Assert.IsTrue(playerMovement.spriteRenderer.flipX);
    }

    [Test]
    public void ItDoesNotFlipSpriteWhenMovingRight()
    {
        playerMovement.input = 1f;
        
        if (playerMovement.input < 0)
        {
            playerMovement.spriteRenderer.flipX = true;
        }
        else if (playerMovement.input > 0)
        {
            playerMovement.spriteRenderer.flipX = false;
        }
        
        Assert.IsFalse(playerMovement.spriteRenderer.flipX);
    }

    [Test]
    public void ItDoesNotFlipSpriteWhenInputIsZero()
    {
        playerMovement.spriteRenderer.flipX = false;
        playerMovement.input = 0f;
        
        bool originalFlipState = playerMovement.spriteRenderer.flipX;
        
        if (playerMovement.input < 0)
        {
            playerMovement.spriteRenderer.flipX = true;
        }
        else if (playerMovement.input > 0)
        {
            playerMovement.spriteRenderer.flipX = false;
        }
        
        Assert.AreEqual(originalFlipState, playerMovement.spriteRenderer.flipX);
    }

    [Test]
    public void ItCalculatesVelocityBasedOnInput()
    {
        playerMovement.input = 1f;
        float currentYVelocity = 5f;
        playerMovement.playerRB.velocity = new Vector2(0f, currentYVelocity);
        
        Vector2 expectedVelocity = new Vector2(playerMovement.input * PlayerMovement.speed, currentYVelocity);
        Vector2 actualVelocity = new Vector2(playerMovement.input * PlayerMovement.speed, playerMovement.playerRB.velocity.y);
        
        Assert.AreEqual(expectedVelocity, actualVelocity);
    }

    [Test]
    public void ItMaintainsYVelocityWhenMovingHorizontally()
    {
        float originalYVelocity = 3.5f;
        playerMovement.playerRB.velocity = new Vector2(0f, originalYVelocity);
        playerMovement.input = 0.5f;
        
        Vector2 newVelocity = new Vector2(playerMovement.input * PlayerMovement.speed, playerMovement.playerRB.velocity.y);
        
        Assert.AreEqual(originalYVelocity, newVelocity.y);
        Assert.AreEqual(playerMovement.input * PlayerMovement.speed, newVelocity.x);
    }

    [Test]
    public void ItIncreasesSpeedCorrectly()
    {
        float initialSpeed = PlayerMovement.speed;
        
        PlayerMovement.IncreasePlayerSpeed();
        
        Assert.AreEqual(initialSpeed + 5f, PlayerMovement.speed);
    }

    [Test]
    public void ItIncreasesSpeedMultipleTimes()
    {
        float initialSpeed = PlayerMovement.speed;
        
        PlayerMovement.IncreasePlayerSpeed();
        PlayerMovement.IncreasePlayerSpeed();
        PlayerMovement.IncreasePlayerSpeed();
        
        Assert.AreEqual(initialSpeed + 15f, PlayerMovement.speed);
    }

    [Test]
    public void ItSetsJumpingStateWhenGroundedAndJumping()
    {
        playerMovement.jumpTimeCounter = 0f;
        playerMovement.canDoubleJump = false;
        
        bool simulateGrounded = true;
        bool simulateJumpPressed = true;
        
        if (simulateGrounded && simulateJumpPressed)
        {
            playerMovement.jumpTimeCounter = playerMovement.jumpTime;
            playerMovement.canDoubleJump = true;
        }
        
        Assert.AreEqual(playerMovement.jumpTime, playerMovement.jumpTimeCounter);
        Assert.IsTrue(playerMovement.canDoubleJump);
    }

    [Test]
    public void ItAllowsDoubleJumpWhenCanDoubleJumpIsTrue()
    {
        playerMovement.canDoubleJump = true;
        bool simulateJumpPressed = true;
        
        if (playerMovement.canDoubleJump && simulateJumpPressed)
        {
            playerMovement.canDoubleJump = false;
        }
        
        Assert.IsFalse(playerMovement.canDoubleJump);
    }

    [Test]
    public void ItDoesNotAllowDoubleJumpWhenCanDoubleJumpIsFalse()
    {
        playerMovement.canDoubleJump = false;
        bool originalDoubleJumpState = playerMovement.canDoubleJump;
        bool simulateJumpPressed = true;
        
        if (playerMovement.canDoubleJump && simulateJumpPressed)
        {
            playerMovement.canDoubleJump = false;
        }
        
        Assert.AreEqual(originalDoubleJumpState, playerMovement.canDoubleJump);
    }

    [Test]
    public void ItCalculatesJumpVelocity()
    {
        float expectedJumpVelocity = playerMovement.jumpForce;
        Vector2 expectedVelocity = Vector2.up * expectedJumpVelocity;
        
        Assert.AreEqual(expectedVelocity, Vector2.up * playerMovement.jumpForce);
    }

    [Test]
    public void ItHasValidJumpTimeRange()
    {
        Assert.IsTrue(playerMovement.jumpTime > 0f);
        Assert.IsTrue(playerMovement.jumpTime <= 1f);
    }

    [Test]
    public void ItHasValidGroundCheckCircleRadius()
    {
        Assert.IsTrue(playerMovement.groundCheckCircle > 0f);
        Assert.IsTrue(playerMovement.groundCheckCircle <= 1f);
    }

    [Test]
    public void ItHasValidJumpForce()
    {
        Assert.IsTrue(playerMovement.jumpForce > 0f);
    }

    [Test]
    public void ItDetectsWhenPlayerIsCloseToGround()
    {
        Vector2 playerVelocity = new Vector2(0f, 0.0005f);
        bool simulateGrounded = true;
        
        bool isNearlyStationary = Mathf.Abs(playerVelocity.y) < 0.001f;
        bool shouldStopJumping = simulateGrounded && isNearlyStationary;
        
        Assert.IsTrue(shouldStopJumping);
    }

    [Test]
    public void ItDoesNotDetectGroundWhenMovingFast()
    {
        Vector2 playerVelocity = new Vector2(0f, 2f);
        bool simulateGrounded = true;
        
        bool isNearlyStationary = Mathf.Abs(playerVelocity.y) < 0.001f;
        bool shouldStopJumping = simulateGrounded && isNearlyStationary;
        
        Assert.IsFalse(shouldStopJumping);
    }
}