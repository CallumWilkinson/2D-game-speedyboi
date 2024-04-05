using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTracker : MonoBehaviour
{
    public static TextMeshProUGUI TMPComponent;
    public static int currentLevel = 1;
    public static int previousLevel = currentLevel - 1;

    void Awake()
    {
        if (TMPComponent == null)
        {
            TMPComponent = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        }
      
        if (TMPComponent.text == "")
        {
            TMPComponent.text = "Level 1";
        }    

    }

}
