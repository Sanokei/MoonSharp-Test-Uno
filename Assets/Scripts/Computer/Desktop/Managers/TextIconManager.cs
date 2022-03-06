using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InGameCodeEditor;

public class TextIconManager : MonoBehaviour
{
    public CodeEditorTheme[] codeEditorTheme;
    public enum editorThemeNames{light, dark, terminal};
    public CodeLanguageTheme[] codeLanguageTheme;
    public enum languageThemeNames{json, lua, txt, cs, ms};
    // Csharp and Miniscript is just for future proofing.
    void Start()
    {
        DesktopManager.OnSetSlotEvent += SetSlot;
        DesktopManager.OnCreateWindowEvent += CreateWindow;
    }

    void SetSlot(int index, Icon icon)
    {
        if(icon is TextIcon textIcon)
        {
            DesktopManager._Slots[index].textObject.text += $".{textIcon.textType.ToString()}";
        }
    }
    // FIXME: All of this is super slow..
    // WARNING: This drops the FPS by a lot!!!!
    /// <summary>
    /// Opens the inventory.
    /// </summary>
    /// <param name="textIcon">The icon that was clicked.</param>
    public void CreateWindow(IconInventorySlot slot)
    {
        Icon icon;
        if(DesktopManager.DesktopInventory.GetIcon(slot.index, out icon))
            if(icon is TextIcon textIcon)
                StartCoroutine((IEnumerator)CreateWindowRoutine(slot, textIcon));
    }
    private IEnumerator CreateWindowRoutine(IconInventorySlot slot, TextIcon textIcon)
    {
        // Createw Physical Representation of Window
        GameObject window = Instantiate(Resources.Load($"Computer/Window/Window_textEditor") as GameObject, transform.parent);
        
        // Get the WindowMaker
        WindowMaker wm = window.GetComponent<WindowMaker>();
        
        // Set the window's title.
        wm.text.text = $"{textIcon.name}.{textIcon.textType.ToString()}";

        // Edit Physical Representation of Window
        wm.CreateWindow(textIcon);

        // DragUI
        wm.dragUI._Camera = Options.Instance._Camera;
        wm.dragUI._Canvas = Options.Instance._Canvas;
        wm.dragUI._CanvasRectTransform = Options.Instance._CanvasRectTransform;
        
        // Set the window's parent.
        window.transform.SetParent(transform.parent);

        // Change Code Editor
        foreach(editorThemeNames name in System.Enum.GetValues(typeof(editorThemeNames)))
        {
            if(name.ToString().ToLower() == Options.Instance.defaultTheme.ToString().ToLower())
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
