using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameCodeEditor;
using TMPro;

public class WindowMaker : MonoBehaviour
{
    [SerializeField] CodeEditor codeEditor;
    [SerializeField] TextMeshProUGUI text;
    public void CreateWindow(TextIcon textIcon){
        codeEditor.Text = textIcon.FileData;
        text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";
    }
}
