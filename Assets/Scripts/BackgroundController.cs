using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour

   
{
    public Sprite backgroundSprite;

    // Start is called before the first frame update
    private void Start()
    {
        //create new gameobject
        GameObject backgroundObject = new GameObject("Background");

        //add Spriterenderer component
        SpriteRenderer renderer = backgroundObject.AddComponent<SpriteRenderer>();

        renderer.sprite = backgroundSprite;
       

    }


}
