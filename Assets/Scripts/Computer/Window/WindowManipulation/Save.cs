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
            //FIXME: Using split all over the place will blow up in my face 
            SeralizedJSON<TextIcon>.SaveScriptableObject(ti,filename.text.Split(".")[0], filename.text.Split(".")[1]);

        }
    }
}