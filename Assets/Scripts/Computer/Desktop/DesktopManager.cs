using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    public List<Slot> inventorySlots;

    // Use this for initialization
    void Start () {
        inventorySlots = new List<Slot>();
        inventorySlots.AddRange(GameObject.FindObjectsOfType<Slot>());

        // Maintain some order (just in case it gets screwed up).
        inventorySlots.Sort((a, b) => a.index - b.index);
        PopulateInitial();
    }

    public void PopulateInitial()
    {
        for (int i = 0; i < inventorySlots.Count; i++) {
            IconInstance instance;
            // If an object exists at the specified location.
            if (Inventory.Instance.GetIcon(i, out instance)) {
                inventorySlots[i].SetIcon(instance);
            }
        }
    }

    public void Clear() { // Doesnt change the Inventory.Instance
        for (int i = 0; i < inventorySlots.Count; i++) {
            inventorySlots[i].RemoveIcon();
        }
    }
}
