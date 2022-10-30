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
            SeralizedJSON<TextIcon>.LoadScriptableObject(filename.text.Split(".")[0],out ti);
            Debug.Log("Before: " + ti.FileData);
            ti.FileData = text.Text;
            Debug.Log("After : " + ti.FileData);
            SeralizedJSON<TextIcon>.SaveScriptableObject(ti,filename.text.Split(".")[0]);

        }
    }
}