using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SlideshowController : MonoBehaviour
{
    public Sprite[] slideshowImages;
    public string[] slideshowTexts;
    public Image displayImage;
    public TextMeshProUGUI displayText;
    public float slideDuration = 5f;
    private int currentIndex = 0;
    //public string nextSceneName = "MainScene";


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ShowSlideshow());
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


        //load next scene which is start of game
        SceneManager.LoadScene("SampleScene");
    }


 
}
