using UnityEngine;

/// <summary>
/// Holds the main ScriptableObjects that will go into an Inventory.
/// </summary>
[System.Serializable]
public abstract class Icon : ScriptableObject {
    public Sprite image;
    public GameObject physicalRepresentation;
}


