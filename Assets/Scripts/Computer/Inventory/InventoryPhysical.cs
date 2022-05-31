using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryPhysical : MonoBehaviour
{
    // Delegates
    public delegate void OnSetSlot(Inventory inventory);
    public static event OnSetSlot OnSetSlotEvent;
    public delegate void OnRemoveSlot();
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public delegate void OnCreateWindow(Icon icon, IconInventorySlot slot);
    public static event OnCreateWindow OnCreateWindowEvent;
    
    // Instance of the inventory Scriptable Object
    [SerializeField] protected Inventory inventory;
    // Initialize the inventory
    protected List<IconInventorySlot> iconInventorySlots;

    // public: gets used in drag manager

    protected void OnEnable() // Right after Awake in execution order
    {
        // it requires the inventory scriptable object
        DragManager.OnDropEvent += OnDrop;
        DragManager.OnDoubleClickEvent += DoubleClickEvent;
        PlayerComputerConnection.OnSpawnedComputerEvent += SpawnedComputerEvent;
        PopulateInitial();
    }
    // protected virtual void Start() // Obselete in execution order
    // {
    //     PopulateInitial();
    // }

    void SpawnedComputerEvent()
    {
        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        OnSetSlotEvent?.Invoke(inventory);
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    {
        OnRemoveSlotEvent?.Invoke();
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
        
        foreach (IconInventorySlot slot in iconInventorySlots)
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
        {
            Refresh();
            return;
        }
        inventory.GetIcon(iconInventorySlot.index,out icon);

        // If the slot is empty insert it and remove the icon from the start position.
        if(inventory.InsertIcon(closestSlot.index,icon) != -1)
        {
            inventory.RemoveIcon(iconInventorySlot.index);
            inventory.SaveInventory(name);
        }
        // FIXME: this feature wont work if I dont fix the fixme for the closestslot thing
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
        // Make the classes subscribed to this event call the appropriate method if the type is correct...
        if(inventory.GetIcon(slot.index, out icon))
            OnCreateWindowEvent?.Invoke(icon, slot);
    }

    // dont remember why this is needed
    // you would think i dont need it beause every 
    // change i make is then reflected when i save the inventory
    // inside of the on drop event etc
    // but i do need it or it doesnt work and i dont remember why..
    void OnApplicationQuit()
    {
        inventory.SaveInventory(name);
    }
}