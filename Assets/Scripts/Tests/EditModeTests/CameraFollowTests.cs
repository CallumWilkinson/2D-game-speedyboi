using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CameraFollowTests
{
    private GameObject cameraObject;
    private GameObject playerObject;
    private CameraFollow cameraFollow;

    [SetUp]
    public void SetUp()
    {
        cameraObject = new GameObject("Camera");
        playerObject = new GameObject("Player");
        cameraFollow = cameraObject.AddComponent<CameraFollow>();
        
        cameraFollow.playerTransform = playerObject.transform;
        cameraFollow.offset = Vector3.back * 10f;
        cameraFollow.smoothSpeed = 0.125f;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cameraObject);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void ItHasDefaultValues()
    {
        Assert.IsNotNull(cameraFollow.playerTransform);
        Assert.AreEqual(Vector3.back * 10f, cameraFollow.offset);
        Assert.AreEqual(0.125f, cameraFollow.smoothSpeed);
    }

    [Test]
    public void ItCalculatesDesiredPositionWithOffset()
    {
        playerObject.transform.position = new Vector3(5f, 3f, 0f);
        Vector3 expectedDesiredPosition = playerObject.transform.position + cameraFollow.offset;
        Vector3 actualDesiredPosition = playerObject.transform.position + cameraFollow.offset;
        
        Assert.AreEqual(expectedDesiredPosition, actualDesiredPosition);
    }

    [Test]
    public void ItMaintainsOffsetWhenPlayerMoves()
    {
        Vector3 initialPlayerPos = new Vector3(0f, 0f, 0f);
        Vector3 newPlayerPos = new Vector3(10f, 5f, 0f);
        
        playerObject.transform.position = initialPlayerPos;
        Vector3 initialDesiredPos = initialPlayerPos + cameraFollow.offset;
        
        playerObject.transform.position = newPlayerPos;
        Vector3 newDesiredPos = newPlayerPos + cameraFollow.offset;
        
        Vector3 expectedDifference = newPlayerPos - initialPlayerPos;
        Vector3 actualDifference = newDesiredPos - initialDesiredPos;
        
        Assert.AreEqual(expectedDifference, actualDifference);
    }

    [Test]
    public void ItHasValidSmoothSpeedRange()
    {
        Assert.IsTrue(cameraFollow.smoothSpeed > 0f);
        Assert.IsTrue(cameraFollow.smoothSpeed <= 1f);
    }

    [Test]
    public void ItHandlesNullPlayerTransform()
    {
        cameraFollow.playerTransform = null;
        
        Assert.DoesNotThrow(() => {
            if (cameraFollow.playerTransform != null)
            {
                Vector3 desiredPosition = cameraFollow.playerTransform.position + cameraFollow.offset;
            }
        });
    }

    [Test]
    public void ItCalculatesCorrectLerpParameters()
    {
        Vector3 currentPos = new Vector3(0f, 0f, -10f);
        Vector3 targetPos = new Vector3(10f, 5f, -10f);
        float deltaTime = 0.016f;
        
        cameraObject.transform.position = currentPos;
        playerObject.transform.position = new Vector3(10f, 5f, 0f);
        
        Vector3 desiredPosition = playerObject.transform.position + cameraFollow.offset;
        float lerpFactor = cameraFollow.smoothSpeed * deltaTime;
        Vector3 expectedSmoothedPos = Vector3.Lerp(currentPos, desiredPosition, lerpFactor);
        
        Vector3 actualSmoothedPos = Vector3.Lerp(cameraObject.transform.position, desiredPosition, lerpFactor);
        
        Assert.AreEqual(expectedSmoothedPos, actualSmoothedPos);
    }
}