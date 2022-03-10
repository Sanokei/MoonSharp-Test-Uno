using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

// TODO: this could be static and just be a singleton.
[CreateAssetMenu(menuName = "Inventory/Physical", fileName = "InventoryPhysical.asset")]
[System.Serializable]
[RequireComponent(typeof(Inventory))]
public class InventoryPhysical : ScriptableObject
{
    // Delegates
    public delegate void OnSetSlot(int index, Icon icon);
    public static event OnSetSlot OnSetSlotEvent;
    public delegate void OnRemoveSlot(Icon icon);
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public delegate void OnCreateWindow(Icon icon, IconInventorySlot slot);
    public static event OnCreateWindow OnCreateWindowEvent;
    
    // Instance of the inventory Scriptable Object
    public Inventory inventory;
    [HideInInspector] public List<IconInventorySlot> iconinventorySlots;

    void OnEnable() // Right after Awake in execution order
    {
        DragManager.OnDropEvent += OnDrop;
        DragManager.OnDoubleClickEvent += DoubleClickEvent;
    }
    public void Begin()
    {
        if(inventory == null)
            inventory = Inventory.Instance(name);
        // iconinventorySlots.Sort((a, b) => a.index - b.index);
        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        for (int i = 0; i < inventory.inventory.Length; i++) 
        {
            Icon icon = null;
            // If an object exists at the specified location.
            if(inventory.GetIcon(i, out icon))
            {
                iconinventorySlots[i].SetSlot(icon);
                if(OnSetSlotEvent != null)
                    OnSetSlotEvent(i, icon);
            }
        }
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    {
        for (int i = 0; i < inventory.inventory.Length; i++) 
        {
            Icon icon = null;
            // If an object exists at the specified location.
            inventory.GetIcon(i, out icon);
            iconinventorySlots[i].RemoveSlot(icon);

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
    void OnDrop(IconInventorySlot iconInventorySlot)
    {
        Icon icon;
        float smallestDistance = float.MaxValue;
        IconInventorySlot closestSlot = null;
        // var isnt slow, we guuchi
        // https://stackoverflow.com/questions/5995876/is-using-var-actually-slow-if-so-why
        foreach (var slot in iconinventorySlots)
        {
            if(slot.Equals(iconInventorySlot))
                continue;
            if(slot.distanceToDroppedIcon < smallestDistance)
            {
                smallestDistance = slot.distanceToDroppedIcon;
                closestSlot = slot;
            }
        }
        inventory.GetIcon(iconInventorySlot.index,out icon);

        // If the slot is empty insert it and remove the icon from the start position.
        if(inventory.InsertIcon(closestSlot.index,icon) != -1)
        {
            inventory.RemoveIcon(iconInventorySlot.index);
            inventory.SaveInventory(name);
        }
        /*<proposed feature>*/
            // else if(icon is FolderIcon)
            // {
            //     // insert the icon into the folder, if not full
            //     inventory.RemoveIcon(_StartPosition.index);
            //     inventory.SaveInventory(name);
            // }
        Refresh();
    }

    public void DoubleClickEvent(IconInventorySlot slot)
    {
        Icon icon;
        // Make the classes subscribed to this event call the appropriate method if the type is correct.
        if(OnCreateWindowEvent != null)
            if(inventory.GetIcon(slot.index, out icon))
                OnCreateWindowEvent(icon, slot);
    }
    void OnApplicationQuit()
    {
        inventory.SaveInventory(name);
    }
}
