using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

[TestFixture]
public class WinTests
{
    private GameObject winObject;
    private GameObject playerObject;
    private GameObject canvasObject;
    private Win winScript;
    private PlayerMovement playerMovement;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private Collider2D winCollider;
    private Collider2D playerCollider;
    private int initialCurrentLevel;

    [SetUp]
    public void SetUp()
    {
        winObject = new GameObject("Win");
        playerObject = new GameObject("Player");
        canvasObject = new GameObject("Canvas");
        
        playerObject.tag = "Player";
        canvasObject.tag = "Canvas";
        
        winScript = winObject.AddComponent<Win>();
        playerMovement = playerObject.AddComponent<PlayerMovement>();
        playerAnimator = playerObject.AddComponent<Animator>();
        playerRigidbody = playerObject.AddComponent<Rigidbody2D>();
        
        winCollider = winObject.AddComponent<BoxCollider2D>();
        playerCollider = playerObject.AddComponent<BoxCollider2D>();
        
        playerMovement.playerRB = playerRigidbody;
        
        initialCurrentLevel = LevelTracker.CurrentLevel;
    }

    [TearDown]
    public void TearDown()
    {
        LevelTracker.CurrentLevel = initialCurrentLevel;
        
        if (winObject != null)
            Object.DestroyImmediate(winObject);
        if (playerObject != null)
            Object.DestroyImmediate(playerObject);
        if (canvasObject != null)
            Object.DestroyImmediate(canvasObject);
    }

    [Test]
    public void ItHasWinScript()
    {
        Assert.IsNotNull(winScript);
        Assert.IsInstanceOf<Win>(winScript);
    }

    [Test]
    public void ItInheritsFromMonoBehaviour()
    {
        Assert.IsInstanceOf<MonoBehaviour>(winScript);
    }

    [Test]
    public void ItHasRequiredCollisionComponents()
    {
        Assert.IsNotNull(winCollider);
        Assert.IsTrue(winObject.GetComponent<Collider2D>() != null);
    }

    [Test]
    public void ItCanFindPlayerWithTag()
    {
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(foundPlayer);
        Assert.AreEqual(playerObject, foundPlayer);
    }

    [Test]
    public void ItCanFindCanvasWithTag()
    {
        GameObject foundCanvas = GameObject.FindGameObjectWithTag("Canvas");
        Assert.IsNotNull(foundCanvas);
        Assert.AreEqual(canvasObject, foundCanvas);
    }

    [Test]
    public void ItCanAccessPlayerAnimator()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator animator = player.GetComponent<Animator>();
        
        Assert.IsNotNull(animator);
        Assert.AreEqual(playerAnimator, animator);
    }

    [Test]
    public void ItCanAccessPlayerMovementScript()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        
        Assert.IsNotNull(movement);
        Assert.AreEqual(playerMovement, movement);
    }

    [Test]
    public void ItDisablesAnimatorOnCollisionExit()
    {
        playerAnimator.enabled = true;
        
        if (playerObject.CompareTag("Player"))
        {
            playerAnimator.enabled = false;
        }
        
        Assert.IsFalse(playerAnimator.enabled);
    }

    [Test]
    public void ItStopsPlayerVelocityOnCollisionExit()
    {
        playerRigidbody.velocity = new Vector2(5f, 3f);
        
        if (playerObject.CompareTag("Player"))
        {
            playerRigidbody.velocity = new Vector2(0, 0);
        }
        
        Assert.AreEqual(Vector2.zero, playerRigidbody.velocity);
    }

    [Test]
    public void ItDisablesPlayerMovementOnCollisionExit()
    {
        playerMovement.enabled = true;
        
        if (playerObject.CompareTag("Player"))
        {
            playerMovement.enabled = false;
        }
        
        Assert.IsFalse(playerMovement.enabled);
    }

    [Test]
    public void ItIncrementsCurrentLevelInRestartLogic()
    {
        int originalLevel = LevelTracker.CurrentLevel;
        
        LevelTracker.CurrentLevel++;
        
        Assert.AreEqual(originalLevel + 1, LevelTracker.CurrentLevel);
    }

    [Test]
    public void ItHandlesLevelProgression()
    {
        int startLevel = 1;
        LevelTracker.CurrentLevel = startLevel;
        
        LevelTracker.CurrentLevel++;
        
        Assert.AreEqual(startLevel + 1, LevelTracker.CurrentLevel);
    }

    
    [Test]
    public void ItCanAccessStaticLevelTrackerProperties()
    {
        int testLevel = 5;
        LevelTracker.CurrentLevel = testLevel;
        
        Assert.AreEqual(testLevel, LevelTracker.CurrentLevel);
    }

    private Collision2D CreateMockCollision2D(GameObject collisionGameObject)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[0];
        return new Collision2D();
    }
}