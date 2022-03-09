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
    public delegate void OnSetSlot(int index, Icon icon);
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

            // Not really useful here..
            // if(OnRemoveSlotEvent != null)
            //     OnRemoveSlotEvent(icon);
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
    void OnDrop(PointerEventData eventdata, IconInventorySlot iconInventorySlot)
    {
        Icon icon;
        IconInventorySlot slot = eventdata.lastPress.gameObject.GetComponent<IconInventorySlot>();
        if(slot == null)
            return;

        DesktopInventory.GetIcon(slot.index,out icon);

        // If the slot is empty insert it and remove the icon from the start position.
        if(DesktopInventory.InsertIcon(slot.index,icon) != -1)
        {
            DesktopInventory.RemoveIcon(iconInventorySlot.index);
            DesktopInventory.SaveInventory("DesktopInventory");
        }
        /*<proposed feature>*/
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
        // Make the classes subscribed to this event call the appropriate method if the type is correct.
        if(OnCreateWindowEvent != null)
            OnCreateWindowEvent(slot);
    }
    void OnApplicationQuit()
    {
        DesktopInventory.SaveInventory("DesktopInventory");
    }
}
