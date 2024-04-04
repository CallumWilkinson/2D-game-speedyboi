using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTracker : MonoBehaviour
{
    public static TextMeshProUGUI levelText;
    public static int currentLevel = 1;

    void Awake()
    {
        if (levelText == null)
        {
            levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        }

    }

}
