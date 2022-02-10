using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Icon/Inventory", fileName = "Inventory.asset")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get {
            if (!instance) {
                Inventory[] tmp = Resources.FindObjectsOfTypeAll<Inventory>();
                if (tmp.Length > 0) {
                    instance = tmp[0];
                    Debug.Log("Found inventory as: " + instance);
                } else {
                    Debug.Log("Did not find inventory, loading from file or template.");
                    SaveManager.LoadOrInitializeInventory();
                }
            }

            return instance;
        }
    }
    
    public static void InitializeFromDefault() {
        if (instance) DestroyImmediate(instance);
        Debug.Log("Loading DEFAULT Inventory from " + Application.persistentDataPath);
        instance = Instantiate((Inventory) Resources.Load("Inventory"));
        instance.hideFlags = HideFlags.HideAndDontSave;
    }

    public static void LoadFromJSON(string path) {
        if (instance) DestroyImmediate(instance);
        instance = ScriptableObject.CreateInstance<Inventory>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), instance);
        instance.hideFlags = HideFlags.HideAndDontSave;
    }

    public void SaveToJSON(string path) {
        Debug.LogFormat("Saving inventory to {0}", path);
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }

    /* Inventory START */
    public IconInstance[] inventory;

    public bool SlotEmpty(int index) {
        if (inventory[index] == null || inventory[index].icon == null)
            return true;

        return false;
    }

    // Get an icon if it exists.
    public bool GetIcon(int index, out IconInstance icon) {
        // inventory[index] doesn't return null, so check icon instead.
        if (SlotEmpty(index)) {
            icon = null;
            return false;
        }

        icon = inventory[index];
        return true;
    }

    // Remove an icon at an index if one exists at that index.
    public bool RemoveIcon(int index) {
        if (SlotEmpty(index)) {
            // Nothing existed at the specified slot.
            return false;
        }

        inventory[index] = null;

        return true;
    }

    // Push an icon, return the index where it was inserted.  -1 if error.
    public int PushIcon(IconInstance icon) {
        for (int i = 0; i < inventory.Length; i++) {
            if (SlotEmpty(i)) {
                inventory[i] = icon;
                return i;
            }
        }

        // Couldn't find a free slot.
        return -1;
    }

    public int InsertIcon(int index, IconInstance icon){
        if(SlotEmpty(index)){
            inventory[index] = icon;
            return index;
        }
        // Was not a free slot.
        return -1;
    }

    // Simply save.
    private void Save() {
        SaveManager.SaveInventory();
    }
}
