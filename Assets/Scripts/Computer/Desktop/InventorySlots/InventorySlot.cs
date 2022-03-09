using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public abstract class InventorySlot<T> : MonoBehaviour
{
    public int index;
    public TextMeshProUGUI textObject;
    public Image imageObject;
    public GameObject PhysicalRepresentation;
}
