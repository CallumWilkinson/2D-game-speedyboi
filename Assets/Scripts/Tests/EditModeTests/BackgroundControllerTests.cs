using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BackgroundControllerTests
{
    [Test]
    public void BackgroundController_HasPublicBackgroundSpriteField()
    {
        // Arrange & Act
        var controller = new GameObject().AddComponent<BackgroundController>();
        
        // Assert
        Assert.IsNotNull(controller);
        
        // Verify the field exists and is accessible
        var field = typeof(BackgroundController).GetField("backgroundSprite");
        Assert.IsNotNull(field, "backgroundSprite field should exist");
        Assert.AreEqual(typeof(Sprite), field.FieldType, "backgroundSprite should be of type Sprite");
        Assert.IsTrue(field.IsPublic, "backgroundSprite should be public for inspector access");
        
        // Clean up
        Object.DestroyImmediate(controller.gameObject);
    }
    
    [Test]
    public void BackgroundController_CanBeAssignedSprite()
    {
        // Arrange
        var controller = new GameObject().AddComponent<BackgroundController>();
        var testSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        
        // Act
        controller.backgroundSprite = testSprite;
        
        // Assert
        Assert.AreEqual(testSprite, controller.backgroundSprite);
        
        // Clean up
        Object.DestroyImmediate(controller.gameObject);
        Object.DestroyImmediate(testSprite);
    }
    
    [Test]
    public void BackgroundController_InitialStateHasNullSprite()
    {
        // Arrange & Act
        var controller = new GameObject().AddComponent<BackgroundController>();
        
        // Assert
        Assert.IsNull(controller.backgroundSprite, "backgroundSprite should be null by default");
        
        // Clean up
        Object.DestroyImmediate(controller.gameObject);
    }
}