using UnityEngine;

// So i made it generic, so it can be used for any type of icon.
// to do that i made it inheritable by specific Inventory types which will fill in the generic type.


// OLD
    // This is programmed as a singleton but its a ScriptableObject.
    // This is because we want to be able to save and load the inventory.
    // But this wont work if we want to have multiple inventories.
    // however sciptableobjects might not count as non-singletons.

    // but scriptableobjects aren't serializable(?) which is pretty stupid if true.
    // so we need to make a custom serializable class.
    // idk if thats how it works
    // but it works.

    // --DONE--
    // TODO: replace all the Json stuff with the:
    // JsonUtility.FromJsonOverwrite(stringJson, scriptableObject);
    // JsonUtility.ToJson(scriptableObject);
    // --END_DONE--
public class Inventory<T> : ScriptableObject
{
    public bool useAsDefault = false;
    public static Inventory<T> Instance {
        get {
            Inventory<T>[] tmp = Resources.FindObjectsOfTypeAll<Inventory<T>>();
            foreach (Inventory<T> ins in tmp) {
                if(ins.useAsDefault) {
                    Debug.Log("Found inventory as: " + ins);
                    ins.hideFlags = HideFlags.HideAndDontSave;
                    instance = ins;
                    return ins;
                }
            }
            Debug.Log("Did not find inventory, loading from file or template.");
            return SaveManager<T>.LoadOrInitializeInventory();
        }
    }
    public T[] InventorySlots{
        get{
            return (T[])(instance.inventory);
        }
        set{
            instance.inventory = value;
        }
    }
    private static Inventory<T> instance {get; set;}
    [SerializeField] private T[] inventory;

    /// <summary>
    /// Reads the default file and loads it into the inventory.
    /// </summary>
    public static Inventory<T> InitializeFromDefault() {
        Debug.Log("Loading DEFAULT Inventory from " + Application.persistentDataPath);
        instance = Instantiate((Inventory<T>) Resources.Load("Inventory"));
        instance.hideFlags = HideFlags.HideAndDontSave;
        return instance;
    }

    /// <summary>
    /// Loads the inventory to a json file.
    /// </summary>
    /// <param name="path">The path to the json file.</param>
    public static Inventory<T> LoadFromJSON(string path) {
        Debug.Log("Loading Inventory from " + path);
        // theres a really stupid bug where if the inventory is 
        // of a different type than the default
        // it will be null
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), instance);
        instance.hideFlags = HideFlags.HideAndDontSave;
        return instance;
    }

    /// <summary>
    /// Saves the inventory to a json file.
    /// </summary>
    /// <param name="path">The path to save the json file to.</param>
    public void SaveToJSON(string path) {
        Debug.LogFormat("Saving inventory to {0}", path);
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }

    /* Inventory START */

    /// <summary>
    /// Checks if a slot is empty.
    /// </summary>
    /// <param name="index">The index of the slot to check.</param>
    /// <returns>True if the slot is empty, false if it is not.</returns>
    public bool SlotEmpty(int index) {
        if (inventory[index] == null || inventory[index] == null)
            return true;

        return false;
    }

    /// <summary>
    // Get an icon if it exists.
    /// </summary>
    /// <param name="index">The index of the icon.</param>
    /// <param name="icon">The icon to return.</param>
    /// <returns>True if the icon exists, false if it doesn't.</returns>
    public bool GetIcon(int index, out T icon) {
        // inventory[index] doesn't return null, so check icon instead.
        if (SlotEmpty(index)) {
            icon = default(T);
            return false;
        }

        icon = (T)inventory[index];
        return true;
    }

    /// <summary>
    /// Remove an icon at an index if one exists at that index.
    /// </summary>
    /// <param name="index">The index of the icon to remove.</param>
    /// <returns>True if the icon was removed, false if it didn't exist.</returns>
    public bool RemoveIcon(int index) {
        if (SlotEmpty(index)) {
            // Nothing existed at the specified slot.
            return false;
        }

        inventory[index] = default(T);

        return true;
    }

    /// <summary>
    /// Push an icon, return the index where it was inserted. If the icon already exists, return -1
    /// </summary>
    /// <param name="icon">The icon to insert.</param>
    /// <returns>The index where the icon was inserted. If the icon already exists, return -1.</returns>
    public int PushIcon(T icon) {
        for (int i = 0; i < inventory.Length; i++) {
            if (SlotEmpty(i)) {
                inventory[i] = icon;
                return i;
            }
        }

        // Couldn't find a free slot.
        return -1;
    }

    /// <summary>
    /// Push an icon, return the index where it was inserted. If the icon already exists, return -1
    /// </summary>
    /// <param name="icon">The icon to insert.</param>
    /// <returns>The index where the icon was inserted. If the icon already exists, return -1.</returns>
    public int InsertIcon(int index, T icon){
        if(SlotEmpty(index)){
            inventory[index] = icon;
            return index;
        }
        // Was not a free slot.
        return -1;
    }

    public int GetIndexOfIcon(T icon) {
        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[i].Equals(icon)) {
                return i;
            }
        }

        return -1;
    }
}
