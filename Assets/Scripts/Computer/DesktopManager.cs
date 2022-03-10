using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    
    // Instance of the ScriptableObject
    public List<IconInventorySlot> iconinventorySlots;
    public InventoryPhysical DesktopInventory;
    void Start()
    {
        // FIXME: Bad Solution
        iconinventorySlots = new List<IconInventorySlot>();
        // this is a bad way to do this. Slow.
        iconinventorySlots.AddRange(IconInventorySlot.FindObjectsOfType<IconInventorySlot>());
        iconinventorySlots.Sort((a, b) => a.index - b.index);

        DesktopInventory.iconinventorySlots = iconinventorySlots;
        DesktopInventory.Begin();
    }
}
