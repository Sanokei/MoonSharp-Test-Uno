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
    public Camera _Camera;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;

    void Start()
    {
        // Create an instance of Options from a json in Awake
        if (Instance == null)
            Instance = this;
    }
}
