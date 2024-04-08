using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Win : MonoBehaviour
{
    private Animator animator;
   
    

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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

        yield return new WaitForSeconds(2f);
        LevelTracker.CurrentLevel++;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        LevelTracker.TMPComponent.text = $"Level {LevelTracker.CurrentLevel}";
        SceneManager.LoadScene("L1");


    }
}
