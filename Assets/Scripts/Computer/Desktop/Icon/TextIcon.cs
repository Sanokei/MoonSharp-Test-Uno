using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileSystem;

[System.Serializable]
[CreateAssetMenu(menuName = "Icon/Text", fileName = "TextName.asset")]
public class TextIcon : Icon {
    public enum TextType {
        JSON, Lua, TXT
    }
    public TextType textType;
    
    private string fileData;
    public string FileData {
        get {
            return FileSystem.File.ReadFile(iconName, textType.ToString());
        }
        set {
            fileData = value;
            FileSystem.File.WriteFile(iconName, value, textType.ToString());
        }
    }
}