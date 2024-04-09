using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEditor;

public class LevelTracker : MonoBehaviour
{
    public static TextMeshProUGUI TMPComponentBottomLevelText;
    public static int _currentLevel = 1;
    public static int CurrentLevel
    {
        get 
        {
            return _currentLevel;
        } 
        set
        {
            //when _currentLevel changes, increase players speed
            if (value > 0)
            {
                PlayerMovement.IncreasePlayerSpeed();
            }
            _currentLevel = value;
        }
    }
 

    void Awake()
    {
        if (TMPComponentBottomLevelText == null)
        {
            TMPComponentBottomLevelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        }
      
        if (TMPComponentBottomLevelText.text == "")
        {
            TMPComponentBottomLevelText.text = "Level 1";
        }    

    }

}
