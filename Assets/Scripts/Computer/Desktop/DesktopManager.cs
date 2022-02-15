using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: this could be static and just be a singleton.
public class DesktopManager : MonoBehaviour
{
    public List<Slot> inventorySlots;
    TextIconInventory Instance;

    /// <summary>
    /// The singleton instance of the Inventory.
    /// </summary>
    /// <summary>
    /// Creates a list of slots for the inventory.
    /// </summary>
    // Use this for initialization
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
    
    /// <summary>
    /// Saves the inventory on quit.
    /// </summary>
    void OnApplicationQuit()
    {
        // Save the inventory.
        SaveManager.SaveInventory();
    }
}
