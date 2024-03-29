using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDrop : MonoBehaviour
{
    private bool canFallThrough = false;
    

    // Update is called once per frame
    void Update()
    { 
        if (canFallThrough == true && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            StartCoroutine(FallTimer());
            canFallThrough = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //when colliding with an object OTHER THAN the base platform, player can fall through
        if (!collision.gameObject.CompareTag("BasePlatform"))
        {
            canFallThrough=true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //when leaving a collision with an object OTHER THAN the base plaform, reset to false
        if (!collision.gameObject.CompareTag("BasePlatform"))
        {
            canFallThrough = false;
        }
    }

    IEnumerator FallTimer()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        GetComponent<CapsuleCollider2D>().enabled = true;

    }
}
