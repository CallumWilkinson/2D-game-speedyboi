using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTracker : MonoBehaviour
{
    public static TextMeshProUGUI levelText;
    public static int currentLevel = 1;

    void Start()
    {
        levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();   
    }

}
