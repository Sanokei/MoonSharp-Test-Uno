using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InGameCodeEditor;

public class Minimize : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CodeEditor text;
    [SerializeField] GameObject window;
    [SerializeField] DragUI drag;
    [SerializeField] TextMeshProUGUI filename;
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(window);

        // FIX ME: This is all a shit way to do this
        GameObject bigWindow = Instantiate(Resources.Load($"Computer/Window/Window_textEditor") as GameObject, drag._Canvas.transform.parent);
        CodeEditor cd = bigWindow.GetComponentInChildren<CodeEditor>();
        DragUI bd = bigWindow.GetComponent<DragUI>();
        TextMeshProUGUI fn = bigWindow.GetComponentInChildren<TextMeshProUGUI>();

        bd._Camera = drag._Camera;
        bd._Canvas = drag._Canvas;
        bd._CanvasRectTransform = drag._CanvasRectTransform;
        cd.Text = text.Text;
        fn.text = filename.text;

        cd.EditorTheme = text.EditorTheme;
        cd.LanguageTheme = text.LanguageTheme;

        bigWindow.transform.SetParent(drag._Canvas.transform);
    }
}
