using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    //singleton
    public static Options Instance{get; private set;}
    
    //
    public TextIconManager.editorThemeNames defaultTheme = TextIconManager.editorThemeNames.dark;

    //
    public Camera _Camera;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;

    void Start()
    {
        Instance = this;
    }
}
