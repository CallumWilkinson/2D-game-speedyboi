using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;
using UnityEngine;

public class CDSoundEffectCollision : MonoBehaviour
{
    private AudioSource audioSource;
    public int cooldownTime = 500;
    private bool onCooldown = false;
    private Task _timer = Task.CompletedTask;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //call when another object's collider component makes contact with this object's collider component
    async void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player collides and play sound
        if (_timer.IsCompleted && collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            onCooldown = true;
            _timer = Task.Delay(cooldownTime);
            _timer.Start();
            //await Task.Delay((int)(cooldownTime *1000));
            //onCooldown = false;
        }
        




    }
}
