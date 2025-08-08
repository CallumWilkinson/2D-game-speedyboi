using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Runtime.CompilerServices;

public class SlideshowController : MonoBehaviour
{
    public Sprite[] slideshowImages;
    private string[] slideshowTexts = 
        { "\"Beyond the comfort of hearth and home, the mountains whisper tales of old.\nHere, our journey begins\"",
        "\"As the path climbs, so too does our resolve.\nEach step taking us closer to our goal\"",
        "\"In the heart of the mountains, a fire burns bright.\nHere, we may rest and gather our strength...\"",
        };
    public Image displayImage;
    public TextMeshProUGUI displayText;
    public Button skipButton;
    public float slideDuration = 5f;
    private int currentIndex = 0;
    private Coroutine slideshowCoroutine;

    private void Awake()
    {
        EnsureEventSystemExists();
    }

    private void EnsureEventSystemExists()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystemGO = new GameObject("EventSystem");
            eventSystemGO.AddComponent<EventSystem>();
            eventSystemGO.AddComponent<StandaloneInputModule>();
        }
    }

    private void Start()
    {
        if (skipButton == null)
        {
            Debug.LogError("Skip button is not assigned in SlideshowController!");
            return;
        }
        
        skipButton.onClick.AddListener(SkipIntro);
        slideshowCoroutine = StartCoroutine(ShowSlideshow());
    }

    private IEnumerator ShowSlideshow()
    {
        //Loop through each image in the slideshowImages array
        while (currentIndex < slideshowImages.Length)
        {
            //set current image sprite to current image in the array
            displayImage.sprite = slideshowImages[currentIndex];


            //could use ternary operator here but i find "if else" to be easier to read
            //check if there is text to show for the current image
            if (slideshowTexts.Length > currentIndex)
            {
                displayText.text = slideshowTexts[currentIndex];
            }
            else
            {
                displayText.text = "";
            }

            //wait for a few seconds to go to next image
            yield return new WaitForSeconds(slideDuration);
            //move to next image in array
            currentIndex++;
        }


        //load next scene which is level 1
        SceneManager.LoadScene("L1");
    }

    public void SkipIntro()
    {
        if (slideshowCoroutine != null)
        {
            StopCoroutine(slideshowCoroutine);
        }
        SceneManager.LoadScene("L1");
    }
}
