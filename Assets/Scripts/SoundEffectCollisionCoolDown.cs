using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;
using UnityEngine;

public class SoundEffectCollisionCoolDown : MonoBehaviour
{
    private AudioSource audioSource;
    public float cooldownTime = 30f;
    private bool onCooldown = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //call when another object's collider component makes contact with this object's collider component
    async void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player collides and play sound
        if (!onCooldown && collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            onCooldown = true;
            await Task.Delay((int)(cooldownTime *1000));
            onCooldown = false;
        }
        




    }
}
