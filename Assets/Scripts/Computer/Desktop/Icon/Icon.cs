using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Icon : ScriptableObject {
    public string iconName;
    public GameObject physicalRepresentation;
}

[System.Serializable]
public class IconInstance{
    // Reference to scriptable object "template".
    public Icon icon;

    // Object-specific data.
    public Image image;
    public string fileData;

    public IconInstance(Icon icon, Image image, string fileData){
        this.icon = icon;
        this.image = image;
        this.fileData = fileData;
    }
}