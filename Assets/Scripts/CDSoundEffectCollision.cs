using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CDSoundEffectCollision : MonoBehaviour
{
    private AudioSource audioSource;
    public int cooldownTime = 500;
    private Task timer = Task.CompletedTask;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //call when another object's collider component makes contact with this object's collider component
    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player collides and play sound
        if (timer.IsCompleted && collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            timer = Task.Delay(cooldownTime);
        }
        




    }
}
