using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileSystem;

/// <summary>
/// A variant of the Icon ScriptableObject that is used to represent a file.
/// </summary>
[System.Serializable]
[CreateAssetMenu(menuName = "Icon/Text", fileName = "TextName.asset")]
public class TextIcon : Icon {
    public enum TextType { Json, Lua, txt }
    public TextType textType;
    
    private string fileData;
    public string FileData {
        get {
            // cant return fileData directly, because 
            // when we save the file, it will be overwritten.
            // so we need to send the FILEs data
            return FileSystem.File.ReadFile(name, textType.ToString());
        }
        set {
            fileData = value;
            FileSystem.File.WriteFile(name, value, textType.ToString());
        }
    }
}