using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    public int index = 0;
    public IconInstance iconInstance = null;    // Inventory backend representation.
    public GameObject prefabInstance = null;    // Inventory frontend representation.

    // TODO: it would be better if we used SetActive() etc rather than Instantiate/Destroy.
    // Use this method to set a slot's icon.
    // The slot will automatically instantiate the gameobject associated with the icon.
    public void SetIcon(IconInstance instance) {
        this.iconInstance = instance;
        this.prefabInstance = Instantiate(
            instance.icon.physicalRepresentation,
            transform // Change the transform of the slot to be the same as the icon.
            )
            as GameObject;
    }

    // Remove the icon from the slot, and destroy the associated gameobject.
    public void RemoveIcon() {
        this.iconInstance = null;
        Destroy(this.prefabInstance);
        this.prefabInstance = null;
    }
}
