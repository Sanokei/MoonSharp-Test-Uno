using UnityEngine;
using System.IO;

/* This was all pretty stupid */

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
[CreateAssetMenu(menuName = "Inventory/Inventory", fileName = "Inventory.asset")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    [SerializeField] private Icon[] inventory;

    // using an indexer, isnt needed in this context
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/
    // public Icon this[int index]
    // {
    //     get
    //     {
    //         return inventory[index];
    //     }
    //     set
    //     {
    //         inventory[index] = value;
    //     }
    // }
    private static Inventory _instance;
    public static Inventory Instance(string filename)
    {
        _instance = _instance ?? LoadInventory(filename); 
        return _instance;
    }

    /// <summary>
    /// Loads the inventory to a json file.
    /// </summary>
    /// <param name="path">The path to the json file.</param>
    public static void LoadFromJSON(string path, out Inventory instance) {
        // theres a really stupid bug where if the inventory is 
        // of a different type than the default
        // it will be null
        instance = ScriptableObject.CreateInstance<Inventory>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), instance);
        instance.hideFlags = HideFlags.HideAndDontSave;
    }

    public static void LoadFromScriptableObject(string name, out Inventory instance) {
        Debug.Log($"Loading Inventory {name} from Resources/Computer/Inventory");
        // theres a really stupid bug where if the inventory is 
        // of a different type than the default
        // it will be null
        instance = Instantiate((Inventory) Resources.Load("Computer/Inventory/" + name));
        instance.hideFlags = HideFlags.HideAndDontSave;
    }
    public void SaveToJSON(string path) {
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }
    /* Inventory START */

    /// <summary>
    /// Checks if a slot is empty.
    /// </summary>
    /// <param name="index">The index of the slot to check.</param>
    /// <returns>True if the slot is empty, false if it is not.</returns>
    public bool SlotEmpty(int index) {
        if (inventory[index] == null)
            return true;
        return false;
    }

    /// <summary>
    // Get an icon if it exists.
    /// </summary>
    /// <param name="index">The index of the icon.</param>
    /// <param name="icon">The icon to return.</param>
    /// <returns>True if the icon exists, false if it doesn't.</returns>
    public bool GetIcon(int index, out Icon icon) {
        // inventory[index] doesn't return null, so check icon instead.
        if (SlotEmpty(index)) {
            icon = default(Icon);
            return false;
        }

        icon = inventory[index];
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

        inventory[index] = default(Icon);

        return true;
    }

    /// <summary>
    /// Push an icon, return the index where it was inserted. If the icon already exists, return -1
    /// </summary>
    /// <param name="icon">The icon to insert.</param>
    /// <returns>The index where the icon was inserted. If the icon already exists, return -1.</returns>
    public int PushIcon(Icon icon) {
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
    public int InsertIcon(int index, Icon icon) {
        if(SlotEmpty(index)){
            inventory[index] = icon;
            return index;
        }
        // Was not a free slot.
        return -1;
    }

    public void SaveInventory(string filename) {
        SaveToJSON(Path.Combine(Application.persistentDataPath, filename + ".json"));
    }
    public static Inventory LoadInventory(string filename)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, (filename + ".json"))))
            {
                Debug.Log("Loading Inventory from " + Path.Combine(Application.persistentDataPath, (filename + ".json")));
                Inventory.LoadFromJSON(Path.Combine(Application.persistentDataPath, (filename + ".json")), out _instance);
            }
            else
            {
                try
                {
                    Debug.LogError("Could not load inventory from (" + Path.Combine(Application.persistentDataPath, (filename + ".json")) + ")"+"\nLoading most recent from Resources/Computer/Inventory instead");
                    Inventory.LoadFromScriptableObject(filename, out _instance);
                }
                catch(System.Exception e)
                {
                    Debug.LogAssertion("No inventory file found." + e.Message);
                }
            }
        return _instance;
    }
}
