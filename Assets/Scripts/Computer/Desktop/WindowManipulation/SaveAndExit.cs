using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InGameCodeEditor;

public class SaveAndExit : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] CodeEditor text;
    [SerializeField] TextMeshProUGUI filename;
    [SerializeField] GameObject window;
    public void OnPointerClick(PointerEventData eventData)
    {
        FileSystem.File.WriteFile(filename.text,text.Text);
        Destroy(window);
    }
}
