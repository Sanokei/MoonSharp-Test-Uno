using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InGameCodeEditor;

public class TextIconManager : MonoBehaviour
{
    public CodeEditorTheme[] codeEditorTheme;
    public enum editorThemeNames{light, dark, terminal}; // Dark is the default. 
    public CodeLanguageTheme[] codeLanguageTheme;
    public enum languageThemeNames{json, lua, txt, cs, ms}; // Csharp and Miniscript is just for future proofing. Not actually used meaningfully.

    void Awake()
    {
        InventoryPhysical.OnCreateWindowEvent += CreateWindow;
    }

    // Fixed by making it a coroutine.
        // FIXME: All of this is super slow..
        // WARNING: This drops the FPS by a lot!!!!
    /// <summary>
    /// Opens the inventory.
    /// </summary>
    /// <param name="slot">The icon slot that was clicked.</param>
    public void CreateWindow(Icon icon, IconInventorySlot slot)
    {
        if(icon is TextIcon textIcon)
            StartCoroutine((IEnumerator)CreateWindowRoutine(slot, textIcon));
    }
    private IEnumerator CreateWindowRoutine(IconInventorySlot slot, TextIcon textIcon)
    {
        // Createw Physical Representation of Window
        GameObject window = Instantiate(Resources.Load($"Computer/Window/Window_textEditor") as GameObject, transform.parent);
        
        // Get the WindowMaker
        WindowMaker wm = window.GetComponent<WindowMaker>(); // slow
        DragUI dragUI = slot.PhysicalRepresentation.GetComponent<DragUI>(); // slow
        
        // Set the window's title.
        wm.text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";

        // Edit Physical Representation of Window
        wm.CreateWindow(textIcon);

        // DragUI
        wm.dragUI._Camera = dragUI._Camera;
        wm.dragUI._Canvas = dragUI._Canvas;
        wm.dragUI._CanvasRectTransform = dragUI._CanvasRectTransform;
        
        // Set the window's parent.
        window.transform.SetParent(transform.parent);

        // Change Code Editor
        foreach(editorThemeNames name in System.Enum.GetValues(typeof(editorThemeNames))) // This may become problematic later, if I allow users to create their own theme at runtime.
        {
            if(name.ToString().ToLower() == PlayerOptions.Instance.defaultTheme.ToString().ToLower())
            {
                wm.codeEditor.EditorTheme = (codeEditorTheme[(int)name]);
                break;
            }
        }

        foreach(languageThemeNames name in System.Enum.GetValues(typeof(languageThemeNames)))
        {
            if(name.ToString().ToLower() == textIcon.textType.ToString().ToLower())
            {
                wm.codeEditor.LanguageTheme = (codeLanguageTheme[(int)name]);
                break;
            }
        }
        
        yield return null;
    }
}
