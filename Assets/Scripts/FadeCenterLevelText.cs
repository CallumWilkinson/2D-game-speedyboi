using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeCenterLevelText : MonoBehaviour
{
    public float fadeTime;
    private TextMeshProUGUI fadeAwayText;



    private void Start()
    {
        fadeAwayText = GetComponent<TextMeshProUGUI>();
        fadeAwayText.text = LevelTracker.TMPComponentBottomLevelText.text;
    }

    private void Update()
    {
        if (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
        }
    }
}
