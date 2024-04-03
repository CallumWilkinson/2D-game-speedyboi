using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    //public float fictionModifier = 2f;
    public float iceSpeedMultipler = 1.5f;
    public float iceDeceleration = 0.95f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        //if (rb != null)
        //{
        //    rb.velocity *= fictionModifier;
        //}

        

    }
}
