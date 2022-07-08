using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FileSystem;
using TMPro;
using InGameCodeEditor;

namespace CodeSystem
{
    public class Save : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] CodeEditor text;
        [SerializeField] TextMeshProUGUI filename;
        public void OnPointerClick(PointerEventData eventData)
        {
            FileSystem.File.WriteFile(filename.text,text.Text);
        }
    }
}