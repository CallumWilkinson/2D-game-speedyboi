using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Runtime.CompilerServices;

public class Win : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovementScript;
   
    

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("hasWon", true);
            StartCoroutine(RestartGame());
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("hasWon", false);
        }
    }

    private IEnumerator RestartGame()
    {



        playerMovementScript.enabled = false;
        yield return new WaitForSeconds(3f);
        playerMovementScript.enabled = true;
        
        


        LevelTracker.CurrentLevel++;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        LevelTracker.TMPComponentBottomLevelText.text = $"Level {LevelTracker.CurrentLevel}";
        SceneManager.LoadScene("L1");


    }
}
