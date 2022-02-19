using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public abstract class InventorySlot<T> : IconManager<T>, IPointerClickHandler
{
    public static event OnDoubleClick OnDoubleClickEvent;
    public int index;
    public TextMeshProUGUI textObject;
    public Image imageObject;
    public GameObject PhysicalRepresentation;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("Double Click");
            OnDoubleClickEvent(eventData);
        }
    }
}
