using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: this could be static and just be a singleton.
public class DesktopManager : MonoBehaviour
{
    public List<Slot> inventorySlots;
    TextIconInventory Instance;
    [HideInInspector] public IconManager clicked;

    void Start () 
    {
        Instance = TextIconInventory.Instance;
        inventorySlots = new List<Slot>();
        inventorySlots.AddRange(GameObject.FindObjectsOfType<Slot>());

        // Maintain some order (just in case it gets screwed up).
        inventorySlots.Sort((a, b) => a.index - b.index);
        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        for (int i = 0; i < inventorySlots.Count; i++) 
        {
            TextIcon instance = null;
            // If an object exists at the specified location.
            if (Instance.GetIcon(i, out instance)) {
                inventorySlots[i].SetSlot(instance);
            }
        }
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    { 
        // Doesnt change the Inventory.Instance
        for (int i = 0; i < inventorySlots.Count; i++) {
            inventorySlots[i].RemoveSlot();
        }
    }

    /// <summary>
    /// Removes the icon from all slots then populates the inventory with the specified icons.
    /// </summary>
    public void Refresh() 
    {
        // There's probably a better way to do this.
        // TODO: This is a bit of a hack.
        Clear();
        PopulateInitial();
    }

    // FIXME: All of this is super slow..
    /// <summary>
    /// Opens the inventory.
    /// </summary>
    /// <param name="textIcon">The icon that was clicked.</param>
    public void OpenTextEditor(TextIcon textIcon)
    {
        //WindowJsonEditor, WindowLuaEditor
        string windowType = $"Window{textIcon.textType.ToString()}Editor";
        Debug.Log(windowType);
        GameObject window = Instantiate(Resources.Load($"Computer/Window/{windowType}") as GameObject, transform.parent);
        
        window.GetComponent<WindowMaker>().CreateWindow(textIcon);

        // ew.
        window.GetComponent<DragUI>()._Camera = Camera.main; // TODO: this is a hack.
        window.GetComponent<DragUI>()._Canvas = GameObject.Find("Screen Canvas").GetComponent<Canvas>();
        window.GetComponent<DragUI>()._CanvasRectTransform = window.GetComponent<DragUI>()._Canvas.GetComponent<RectTransform>();

        window.transform.SetParent(transform.parent);
    }
    
    /// <summary>
    /// Saves the inventory on quit.
    /// </summary>
    void OnApplicationQuit()
    {
        // Save the inventory.
        SaveManager.SaveInventory();
    }
}
