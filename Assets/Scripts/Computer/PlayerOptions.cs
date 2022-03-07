using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerOptions : MonoBehaviour
{
    //singleton
    public static PlayerOptions Instance{get; private set;}
    
    //
    public TextIconManager.editorThemeNames defaultTheme = TextIconManager.editorThemeNames.dark;
    public bool defaultShowReticle = true;

    void Start()
    {
        // Create an instance of Options from a json in Awake
        if (Instance == null)
            Instance = this;
    }
}
