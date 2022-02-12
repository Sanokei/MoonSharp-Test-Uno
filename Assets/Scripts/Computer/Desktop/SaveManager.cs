using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager
{
    // TODO: The names of the json are hardcoded, gotta make it dynamic and pass the name through when it is called.
    // this is a terrible hack and should be fixed.
    // pretty dumb since its static but it needs its own name which means it should just call its own method itself
    // it works for now.

    // Making the SaveManager was kinda worthless.

    /// <summary>
    /// Loads the specified json file.
    /// if the file doesnt exist, it will create a new one.
    /// </summary>
    public static IconInventory LoadOrInitializeInventory() 
    {
        // Saving and loading.
        if (File.Exists(Path.Combine(Application.persistentDataPath, "inventory.json")))
        {
            return (IconInventory) IconInventory.LoadFromJSON(Path.Combine(Application.persistentDataPath, "inventory.json"));
        } 
        else 
        {
            return (IconInventory) IconInventory.InitializeFromDefault();
        }
    }

    /// <summary>
    /// Saves the inventory to a json file.
    /// </summary>
    public static void SaveInventory() 
    {
        IconInventory.Instance.SaveToJSON(Path.Combine(Application.persistentDataPath, "inventory.json"));
    }


    // Load from the default, for situations where we just want to reset.
    public static void LoadFromTemplate() 
    {
        IconInventory.InitializeFromDefault();
    }
}