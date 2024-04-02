using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    public float fictionModifier = 0.98f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null )
        {
            rb.velocity *= fictionModifier;
        }
    }
}
