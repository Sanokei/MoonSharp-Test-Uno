using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Icon/Text", fileName = "TextName.asset")]
public class TextIcon : Icon {
    public enum TextType {
        JSON, Lua, txt
    }

    public TextType textType;
}