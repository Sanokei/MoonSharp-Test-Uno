using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class TextIconInventorySlot : InventorySlot<TextIcon>, IPointerClickHandler
{
    public static event OnSetSlot OnSetSlotEvent;
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public static event OnDoubleClick OnDoubleClickEvent;
    public override void SetSlot(TextIcon icon)
    {
        PhysicalRepresentation.SetActive(true);
        textObject.text = $"{icon.name}.{icon.textType.ToString()}";
        imageObject.sprite = icon.image;
        OnSetSlotEvent(icon);
    }
    
    public override void RemoveSlot(TextIcon icon)
    {
        PhysicalRepresentation.SetActive(false);
        textObject.text = $"filename.{icon.textType.ToString()}";
        imageObject.sprite = null;
        OnRemoveSlotEvent(icon);
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("Double Click");
            TextIcon icon = null;
            Inventory<TextIcon>.Instance.GetIcon(index, out icon);
            OnDoubleClickEvent(icon);
        }
    }
}
