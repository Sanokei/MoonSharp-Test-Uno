using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Inventory inventory;

    // public: gets used in drag manager
    [HideInInspector] public List<IconInventorySlot> iconinventorySlots;

    void OnEnable() // Right after Awake in execution order
    {
        // it requires the inventory scriptable object
        if(inventory == null) // gaurd clause
            Debug.LogAssertion("InventoryPhysical requires an Inventory scriptable object.");
        DragManager.OnDropEvent += OnDrop;
        DragManager.OnDoubleClickEvent += DoubleClickEvent;
    }
    void OnDisable() // Right before Destroy in execution order
    {
        DragManager.OnDropEvent -= OnDrop;
        DragManager.OnDoubleClickEvent -= DoubleClickEvent;
    }
    public void Begin()
    {
        inventory = inventory ?? Inventory.Instance(name);
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
                OnSetSlotEvent?.Invoke(i, icon);
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
            // This should be some sort of "should assemble" 
            // flag that gets called etc etc
        Clear();
        PopulateInitial();
    }
    void OnDrop(IconInventorySlot iconInventorySlot)
    {
        Icon icon;
        float smallestDistance = float.MaxValue;
        IconInventorySlot closestSlot = null;
        // var isnt slow, we guuchi
        // still not gonna use it for readability sake xd
        // https://stackoverflow.com/questions/5995876/is-using-var-actually-slow-if-so-why
        
        foreach (IconInventorySlot slot in iconinventorySlots)
        {
            if(slot.distanceToDroppedIcon < smallestDistance)
            {
                smallestDistance = slot.distanceToDroppedIcon;
                closestSlot = slot;
            }
        }
        // ^
        // FIXME: this is inefficient.
            // This is a terrible way to do this but it works.
            // Im getting the values and testing which one has the least distance
            // and then setting the icon to that slot.
            // This is so slow and dumb, but it works and I cant be bothered
            // to make it better.
        
        // ----------

        // means it was picked up then put back down
        if(closestSlot.Equals(iconInventorySlot))
            Refresh();

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
        // if(OnCreateWindowEvent == null) return; // gaurd clause
        if(inventory.GetIcon(slot.index, out icon))
            OnCreateWindowEvent?.Invoke(icon, slot);
    }

    // dont remember why this is needed
    // you would think i dont need it beause every 
    // change i make is then reflected when i save the inventory
    // inside of the on drop event etc
    // but i do need it or it doesnt work and i dont remember why..
    // too bad!
    void OnApplicationQuit()
    {
        inventory.SaveInventory(name);
    }
}
