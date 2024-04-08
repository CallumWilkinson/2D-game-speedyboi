using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCenterLevelText : MonoBehaviour
{
    private Animator animatior;

    private void Start()
    {
        animatior = GetComponent<Animator>();
        animatior.Play("TextFadeOut");
    }
}
