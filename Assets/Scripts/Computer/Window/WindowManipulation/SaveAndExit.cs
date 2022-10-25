using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InGameCodeEditor;
using SeralizedJSONSystem;

public class SaveAndExit : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] CodeEditor text;
    [SerializeField] TextMeshProUGUI filename;
    [SerializeField] GameObject window;
    public void OnPointerClick(PointerEventData eventData)
    {
        // filename.text already has the fileTag attached
        TextIcon ti;
        SeralizedJSON<TextIcon>.LoadScriptableObject(filename.text,out ti);
        ti.FileData = text.Text;
        SeralizedJSON<TextIcon>.SaveScriptableObject(ti,filename.text);
        SeralizedJSON<TextIcon>.SaveScriptableObject(ti,filename.text.Split(".")[0], filename.text.Split(".")[1]);
        Destroy(window);
    }
}
