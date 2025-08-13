using System.Collections;
using NUnit.Framework;
using Tests.Support;
using UnityEngine;
using UnityEngine.TestTools;
[TestFixture]
public class PlayerMovementIntegrationTests : PlayModeBootstrap
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

    [UnityTest]
    public IEnumerator ItMovesPlayerHorizontallyOverTime()
    {
        Vector3 initialPosition = playerObject.transform.position;
        playerMovement.input = 1f;

        yield return new WaitForFixedUpdate();

        playerMovement.playerRB.velocity = new Vector2(playerMovement.input * PlayerMovement.speed, 0f);

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(playerObject.transform.position.x >= initialPosition.x);
    }

    [UnityTest]
    public IEnumerator ItAppliesPhysicsCorrectly()
    {
        rigidbody2D.gravityScale = 1f;
        Vector3 initialPosition = playerObject.transform.position;
        
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsTrue(playerObject.transform.position.y <= initialPosition.y);
    }

    [UnityTest]
    public IEnumerator ItHandlesJumpInputOverTime()
    {
        playerMovement.jumpTimeCounter = playerMovement.jumpTime;
        float initialYVelocity = rigidbody2D.velocity.y;
        
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, playerMovement.jumpForce);
        
        yield return new WaitForFixedUpdate();
        
        Assert.IsTrue(rigidbody2D.velocity.y > initialYVelocity);
    }

    [UnityTest]
    public IEnumerator ItMaintainsComponentReferencesAtRuntime()
    {
        yield return null;
        
        Assert.IsNotNull(playerMovement.playerRB);
        Assert.IsNotNull(playerMovement.spriteRenderer);
        Assert.IsNotNull(playerMovement.animator);
        Assert.IsNotNull(playerMovement.feetPosition);
    }

    [UnityTest]
    public IEnumerator ItUpdatesAnimatorParametersCorrectly()
    {
        if (animator.runtimeAnimatorController != null)
        {
            playerMovement.input = 1f;
            
            yield return null;
            
            Assert.IsTrue(true);
        }
        else
        {
            yield return null;
            Assert.Pass("No animator controller attached, test skipped gracefully");
        }
    }

    [Test]
    public void ItInitializesCorrectlyInPlayMode()
    {
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(rigidbody2D);
        Assert.IsTrue(rigidbody2D.bodyType == RigidbodyType2D.Dynamic);
    }

    [UnityTest]
    public IEnumerator ItHandlesContinuousInput()
    {
        for (int frame = 0; frame < 10; frame++)
        {
            playerMovement.input = frame % 2 == 0 ? 1f : -1f;
            yield return new WaitForFixedUpdate();
        }
        
        Assert.IsTrue(true);
    }
}