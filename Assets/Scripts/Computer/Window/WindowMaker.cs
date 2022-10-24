using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InGameCodeEditor;
using TMPro;

public class WindowMaker : MonoBehaviour
{
    void OnEnable()
    {
        ComputerManager.DeactivateInputFieldEvent += DeactivateInputField;
    }
    void OnDisable()
    {
        ComputerManager.DeactivateInputFieldEvent -= DeactivateInputField;
    }
    public CodeEditor codeEditor;
    public TextMeshProUGUI text;
    public DragUI dragUI;
    public TMP_InputField TMP_inputField;
    public void CreateWindow(TextIcon textIcon){
        codeEditor.Text = textIcon.FileData;
        text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";
    }

    // Fix for https://github.com/SidewaysTurtle/HackPunk/issues/5
    void DeactivateInputField()
    { 
        TMP_inputField.DeactivateInputField();
    }

    public void setDragUI(Camera camera, Canvas canvas, RectTransform canvasRectTransform)
    {
        dragUI._Camera = camera;
        dragUI._Canvas = canvas;
        dragUI._CanvasRectTransform = canvasRectTransform;
    }
}
