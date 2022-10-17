using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InGameCodeEditor;
using SeralizedJSONSystem;

namespace CodeSystem
{
    public class Save : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] CodeEditor text;
        [SerializeField] TextMeshProUGUI filename;
        public void OnPointerClick(PointerEventData eventData)
        {
            TextIcon ti;
            SeralizedJSON<TextIcon>.LoadScriptableObject(filename.text,out ti);
            ti.FileData = text.Text;
            SeralizedJSON<TextIcon>.SaveScriptableObject(ti,filename.text);
        }
    }
}