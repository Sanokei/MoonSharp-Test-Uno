using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Icon : ScriptableObject {
    public string iconName;
    public Image image;
    public GameObject physicalRepresentation;
}

[System.Serializable]
public class IconInstance{
    // Reference to scriptable object "template".
    public Icon icon;

    // Object-specific data.
    public Image image;

    public IconInstance(Icon icon, Image image, string filename){
        this.icon = icon;
        this.image = image;
        this.icon.iconName = filename;
    }
}