using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

// TODO: this could be static and just be a singleton.
[System.Serializable]
public class DesktopManager : MonoBehaviour
{
    //
    private static IconInventorySlot _StartPosition;

    // Delegates
    public delegate void  OnSetSlot(int index, Icon icon);
    public static event OnSetSlot OnSetSlotEvent;
    public delegate void OnRemoveSlot(Icon icon);
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public delegate void OnCreateWindow(IconInventorySlot slot);
    public static event OnCreateWindow OnCreateWindowEvent;
    
    //
    public static Inventory DesktopInventory;

    // Instance of the ScriptableObject
    public static List<IconInventorySlot> _Slots;

    void Awake()
    {
        DragManager.OnBeginDragEvent += OnBeginDrag;
        DragManager.OnDropEvent += OnDrop;
        IconInventorySlot.OnDoubleClickEvent += DoubleClickEvent;
    }
    void Start()
    {
        if(DesktopInventory == null)
            DesktopInventory = Inventory.Instance("DesktopInventory");
        _Slots = new List<IconInventorySlot>();
        // this is a bad way to do this.
        _Slots.AddRange(IconInventorySlot.FindObjectsOfType<IconInventorySlot>());
        _Slots.Sort((a, b) => a.index - b.index);

        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        for (int i = 0; i < DesktopInventory.inventory.Length; i++) 
        {
            Icon icon = null;
            // If an object exists at the specified location.
            if(DesktopInventory.GetIcon(i, out icon))
            {
                _Slots[i].SetSlot(icon);
                OnSetSlotEvent(i, icon);
            }
        }
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    {
        for (int i = 0; i < DesktopInventory.inventory.Length; i++) 
        {
            Icon icon = null;
            // If an object exists at the specified location.
            DesktopInventory.GetIcon(i, out icon);
            _Slots[i].RemoveSlot(icon);
            OnRemoveSlotEvent(icon);
        }
    }

    /// <summary>
    /// Removes the icon from all slots then populates the inventory with the specified icons.
    /// </summary>
    public void Refresh() 
    {
        // TODO: This is a bit of a hack.
        Clear();
        PopulateInitial();
    }

    void OnBeginDrag(IconInventorySlot slot)
    {
        _StartPosition = slot;
    }
    void OnDrop(int index)
    {
        Icon icon;
        DesktopInventory.GetIcon(_StartPosition.index,out icon);
        if(DesktopInventory.InsertIcon(index,icon) != -1)
        {
            DesktopInventory.RemoveIcon(_StartPosition.index);
            DesktopInventory.SaveInventory("DesktopInventory");
        }
        // else if(icon is FolderIcon)
        // {
        //     // insert the icon into the folder, if not full
        //     DesktopInventory.RemoveIcon(_StartPosition.index);
        //     DesktopInventory.SaveInventory("DesktopInventory");
        // }
        Refresh();
    }

    public void DoubleClickEvent(IconInventorySlot slot)
    {
        OnCreateWindowEvent(slot);
    }
    void OnApplicationQuit()
    {
        DesktopInventory.SaveInventory("DesktopInventory");
    }
}
