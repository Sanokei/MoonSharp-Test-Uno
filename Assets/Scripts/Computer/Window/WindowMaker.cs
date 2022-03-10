using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameCodeEditor;
using TMPro;

public class WindowMaker : MonoBehaviour
{
    void Awake()
    {
        Eyes.DeactivateInputFieldEvent += DeactivateInputField;
    }
    public CodeEditor codeEditor;
    public TextMeshProUGUI text;
    public DragUI dragUI;
    public TMP_InputField inputField;
    public void CreateWindow(TextIcon textIcon){
        codeEditor.Text = textIcon.FileData;
        text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";
    }
    void DeactivateInputField(){
        inputField.DeactivateInputField();
    }
}
