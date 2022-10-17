using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InGameCodeEditor;

using UnityEngine.SceneManagement;

using System.IO;

public class TextIconManager : MonoBehaviour
{
    public CodeEditorTheme[] codeEditorTheme;
    public enum editorThemeNames{light, dark, terminal}; // Dark is the default. 
    public CodeLanguageTheme[] codeLanguageTheme;
    public enum languageThemeNames{json, lua, txt, cs, ms}; // Csharp and Miniscript is just for future proofing. Not actually used meaningfully.
    public string file_path;
    void Awake()
    {
        InventoryPhysical.OnCreateWindowEvent += CreateWindow;
        file_path = Path.Combine(Application.persistentDataPath,SceneManager.GetActiveScene().name,"ingamefiles");
    }

    // Fixed by making it a coroutine (?)
        // FIXME: All of this is super slow..
        // WARNING: This drops the FPS by a lot!!!!
    public void CreateWindow(TextIcon icon, IconInventorySlot slot)
    {
        StartCoroutine((IEnumerator)Co_CreateWindowRoutine(slot, icon));
    }
    private IEnumerator Co_CreateWindowRoutine(IconInventorySlot slot, TextIcon textIcon)
    {
        // Createw Physical Representation of Window
        GameObject window = Instantiate(Resources.Load($"Computer/Window/Window_textEditor") as GameObject, transform.parent);
        
        // FIXME: This is super slow. Dont know how I would fix it.

        // Get the WindowMaker
        WindowMaker wm = window.GetComponent<WindowMaker>(); // slow
        DragUI dragUI = slot.PhysicalRepresentation.GetComponent<DragUI>(); // slow
        
        // Set the window's title.
        wm.text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";

        // Edit Physical Representation of Window
        wm.CreateWindow(textIcon);

        // Setting DragUI
        wm.setDragUI(dragUI._Camera, dragUI._Canvas, dragUI._CanvasRectTransform);
        
        // Set the window's parent.
        window.transform.SetParent(transform.parent);

        // Change Code Editor
        foreach(editorThemeNames name in System.Enum.GetValues(typeof(editorThemeNames))) // TODO: This may become problematic later, if I allow users to create their own theme at runtime.
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
