using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectCollison : MonoBehaviour
{
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //call when another object's collider component makes contact with this object's collider component
    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player collides and play sound
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        
        }





    }
}
